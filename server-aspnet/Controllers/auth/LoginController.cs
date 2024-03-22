using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Server.Data;
using Server.ViewModels;
using Server.Interface;
using Server.Models;
using System.Data;

namespace Server.Controllers
{
    [Route("/auth/login")]
    public class LoginController : Controller
    {
        private ILogger<LoginController> _logger;
        private readonly UserManagerDB _context;
        private readonly IHashService _hashService;
        private readonly IEmailService _emailManager;
        private readonly ICryptoService _cryptoService;

        // Dependency injection via constructor
        public LoginController(ILogger<LoginController> logger, UserManagerDB context, IHashService hashService, IEmailService emailService, ICryptoService cryptoService)
        {
            _logger = logger;
            _context = context;
            _hashService = hashService;
            _emailManager = emailService;
            _cryptoService = cryptoService;
        }

        // Find a user by username or email
        private async Task<User> FindUserAsync(string usernameOrEmail)
        {
            return await _context.Users
                .Include(u => u.Settings)
                .ThenInclude(s => s.SecuritySettings)
                .SingleOrDefaultAsync(u => u.Email == usernameOrEmail || u.UserName == usernameOrEmail);
        }

        // Verify if the provided password matches the hashed password
        private async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return await _hashService.Compare(password, hashedPassword);
        }

        private async Task<byte[]> EncryptUserId(string userID)
        {
            return await _cryptoService.EncryptAsync(userID);
        }

        // Handle two-factor authentication
        private async Task<IActionResult> _HandleTwoFactorAuthAsync(User user, LoginViewModel model)
        {
            // Generate a random 5-digit number for the authentication code
            string code = new Random().Next(10000, 100000).ToString();

            // Create a dictionary to hold additional data for the email
            Dictionary<string, object> additianalData = new()
            {
                {"CodeForAuth", code}
            };

            // Send an email with the authentication code
            await _emailManager.SendEmailAsync("TwoFactorAuth", "Two Factor Authentication Code", model.UsernameOrEmail, additianalData);

            // Update the TwoFactorAuthCode in the user's security settings
            user.Settings.SecuritySettings.TwoFactorAuthCode = code;

            _context.SaveChanges();

            byte[] encryptedUserId = await EncryptUserId(user.UserId.ToString());

            HttpContext.Session.SetString("ID", encryptedUserId.ToString());

            return Ok(new { twoFactorAwait = true, message = "Email has been send to you with the code" });
        }

        // Define the Login action
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                Console.WriteLine(model.UsernameOrEmail);
                // Check if the model is valid
                if (ModelState.IsValid)
                {
                    User user = await FindUserAsync(model.UsernameOrEmail);

                    if (user == null)
                    {
                        return NotFound(new {existingUser = false, message = "Email or username not found" });
                    }

                    if (!await VerifyPassword(model.Password, user.Password))
                    {
                        user.Settings.SecuritySettings.FailedLoginAttempts += 1;
                        await _context.SaveChangesAsync();

                        return BadRequest(new { incorrectPassword = false, message = "Incorrect password for this user" });
                    }

                    // If two-factor authentication is enabled, handle it
                    if (user.Settings.SecuritySettings.TwoFactorAuth)
                    {
                       await _HandleTwoFactorAuthAsync(user, model);
                    }

                    // Store the user ID in the session
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    return Ok(new { message = "Login successful" });

                }

                // If the model is not valid, return model errors
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Filed = x.Key, Message = x.Value.Errors.First().ErrorMessage })
                    .ToList();
                return StatusCode(500, new { errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login request.");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}