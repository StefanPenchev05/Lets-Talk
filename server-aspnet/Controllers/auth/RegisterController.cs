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
        private readonly ITokenService tokenService;
        private readonly IEmailService emailService;

        public RegisterController(ILogger<RegisterController> logger, UserManagerDB context)
        {
            _logger = logger;
            _context = context;
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

        private async Task<string> GenerateToken(string uuid)
        {
            return await tokenService.GenerateTokenAsync(uuid);
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
                    string verificationCode = Guid.NewGuid().ToString();

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
                        VerificationCode = verificationCode
                    };

                    // Save the changes to the database
                    await _context.SaveChangesAsync();

                    string token = await GenerateToken(tempUser.Id.ToString());
                    
                    await emailService.SendEmailAsync("EmailVerification","Link For Email Verification", model.Email);

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
    }
}