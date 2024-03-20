using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Server NameSpaces
using Server.Data;
using Server.Validator;
using Server.ViewModels;
using Server.Services;
using Server.Models;

namespace Server.Controllers
{
    // Define the route for this controller
    [Route("/auth/login")]
    public class LoginController : Controller
    {
        private ILogger<LoginController> _logger;
        private IConfiguration _configuration;
        private readonly UserManagerDB _context;
        private readonly IHashService _hashService;
        private readonly EmailManager _emailManager;
        private readonly IViewRenderService _viewRenderService;

        public LoginController(ILogger<LoginController> logger, UserManagerDB context, EmailManager emailManager, LoginValidator validator, IConfiguration configuration, IViewRenderService viewRenderService)
        {
            _logger = logger;
            _context = context;
            _emailManager = emailManager;
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }
        private async Task<User> FindUserAsync(string usernameOrEmail)
        {
            return await _context.Users
                .Include(u => u.Settings)
                .ThenInclude(s => s.SecuritySettings)
                .SingleOrDefaultAsync(u => u.Email == usernameOrEmail || u.UserName == usernameOrEmail);
        }

        private async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            return await _hashService.Compare(password, hashedPassword);
        }

        private async Task<IActionResult> _HandleTwoFactorAuthAsync(User user, LoginViewModel model)
        {
            // Generate a random 5-digit number for the authentication code
            int code = new Random().Next(10000, 100000);

            // Create a dictionary to hold additional data for the email
            Dictionary<string, object> additianalData = new()
            {
                {"CodeForAuth", code}
            };

            // Use the EmailManager to send an email with the authentication code and return to the client to check his email and write the code given
            await _emailManager.SendEmailAsync("TwoFactorAuth", "Two Factor Authentication Code", model, additianalData);

            // Update the TwoFactorAuthCode in the user's security settings
            user.Settings.SecuritySettings.TwoFactorAuthCode = code;

            _context.SaveChanges();

            return Ok(new { twoFactorAwait = true, message = "Email has been send to you with the code" });
        }

        // Define the Login action
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                // Check if the model is valid
                if (ModelState.IsValid)
                {
                    User user = await FindUserAsync(model.UsernameOrEmail);

                    if (user == null)
                    {
                        return NotFound(new { message = "User not found" });
                    }

                    if (!await VerifyPassword(model.Password, user.Password))
                    {
                        user.Settings.SecuritySettings.FailedLoginAttempts += 1;
                        await _context.SaveChangesAsync();

                        return BadRequest(new { message = "Incorrect password" });
                    }

                    if (user.Settings.SecuritySettings.TwoFactorAuth)
                    {
                       await _HandleTwoFactorAuthAsync(user, model);
                    }

                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    return Ok(new { message = "Login successful" });

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