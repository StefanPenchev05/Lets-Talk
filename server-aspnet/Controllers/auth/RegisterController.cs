using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.ViewModels;
using Server.Models;
using Microsoft.EntityFrameworkCore;
using Server.Interface;

namespace Server.Controllers
{
    [Route("/auth/register")]
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly UserManagerDB _context;
        private readonly IHashService _hashService;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IAuthHub _authHub;

        public RegisterController(ILogger<RegisterController> logger, UserManagerDB context, IHashService hashService, ITokenService tokenService, IEmailService emailService, IAuthHub authHub, ICryptoService cryptoService)
        {
            _logger = logger;
            _context = context;
            _hashService = hashService;
            _tokenService = tokenService;
            _emailService = emailService;
            _cryptoService = cryptoService;
            _authHub = authHub;
        }

        private async Task<string> SuggestUsername(string username)
        {
            // Initialize a flag to check if the username is taken
            bool isUsernameTaken = true;

            // Initialize a suffix to append to the username if it's taken
            int suffix = 1;

            // Initialize the suggested username with the provided username
            string suggestedUsername = username;

            // Loop until we find a username that's not taken
            while (isUsernameTaken)
            {
                // Try to find a user with the suggested username
                User userWithUsername = await _context.Users
                    .SingleOrDefaultAsync(u => u.UserName == suggestedUsername);

                // If a user with the suggested username exists...
                if (userWithUsername != null)
                {
                    // Append the suffix to the username and increment the suffix
                    suggestedUsername = $"{username}{suffix}";
                    suffix++;
                }
                else
                {
                    // If no user with the suggested username exists, set the flag to false
                    isUsernameTaken = false;
                }
            }

            // Return the suggested username
            return suggestedUsername;
        }

        private async Task<string> GenerateToken(List<string> data, int expiresInMinutes)
        {
            return await _tokenService.GenerateTokenAsync(data, expiresInMinutes);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the email is already taken
                    User userWithEmail = await _context.Users
                        .SingleOrDefaultAsync(u => u.Email == model.Email);

                    if (userWithEmail != null)
                    {
                        return BadRequest(new { emailExists = true, message = "This email is already taken" });
                    }

                    // Check if the username is already taken
                    User userWithUsername = await _context.Users
                        .SingleOrDefaultAsync(u => u.UserName == model.Username);

                    // If a user with the same username already exists...
                    if (userWithUsername != null)
                    {
                        // Generate a suggested username
                        string suggestedUsername = await SuggestUsername(model.Username);

                        // Return a BadRequest response with the error message and the suggested username
                        return BadRequest(new { usernameExists = true, message = "This username is already taken", SuggestUsername = suggestedUsername });
                    }

                    // Hash the password using the hash service
                    string hashedPassword = await _hashService.HashPassword(model.Password);

                    // Generate a new verification code
                    string roomId = Guid.NewGuid().ToString();

                    // Create a new TempData object for the new user
                    TempData tempUser = new()
                    {
                        Email = model.Email,
                        Password = hashedPassword,
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        ProfilePicture = model.ProfilePicture,
                        TwoFactorAuth = model.TwoFactorAuth,
                        VerificationCode = roomId
                    };
                    
                    // Add the tempUser to the DbSet
                    _context.tempDatas.Add(tempUser);

                    // Save the changes to the database
                    await _context.SaveChangesAsync();

                    List<string> dataForToken = new()
                    {
                        roomId,
                        tempUser.Id.ToString()
                    };

                    string token = await GenerateToken(dataForToken, 15);

                    Dictionary<string, object> additionalData = new()
                    {
                        {"VerificationLink", token}
                    };

                    await _emailService.SendEmailAsync("EmailVerification", "Link For Email Verification", model.Email, additionalData);

                    return Ok(new { AwaitForEmailVerification = true, roomId, message = "Sended Email" });

                }
                // If the model is not valid, return model errors
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Filed = x.Key, Message = x.Value.Errors.First().ErrorMessage })
                    .ToList();
                return StatusCode(500, new { message = errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var data = await _tokenService.VerifyTokenAsync(token);

            if (data == null)
            {
                return StatusCode(404, new { invalidToken = true, message = "This token is invalid" });
            }

            var roomId = data[0];
            var tempUserId = data[1];

            var existingTempUser = await _context.tempDatas.SingleOrDefaultAsync(t => t.Id == int.Parse(tempUserId));

            if (existingTempUser == null)
            {
                return StatusCode(404, new { tempUserNotFound = true, message = "Your temporary registration data could not be found or has expired. Please register again." });
            }

            User newUser = new()
            {
                Email = existingTempUser.Email,
                UserName = existingTempUser.UserName,
                FirstName = existingTempUser.FirstName,
                LastName = existingTempUser.LastName,
                Password = existingTempUser.Password,
                ProfilePicture = existingTempUser.ProfilePicture == null ? null : existingTempUser.ProfilePicture,
                Settings = new ()
                {
                    SecuritySettings = new()
                    {
                        TwoFactorAuth = existingTempUser.TwoFactorAuth == false ? false : true
                    }
                }  
            };

            // Add the new User to the DbSet
            _context.Users.Add(newUser);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Sends to the user that his email is verified
            await _authHub.SendToRoom(roomId);

            return Ok();
        }
    }
}