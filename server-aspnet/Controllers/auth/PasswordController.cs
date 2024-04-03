using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Data;
using Server.Services;

namespace Server.Controllers
{
    [Route("/password")]
    public class PasswordController : Controller
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly UserManagerDB _context;
        private readonly EmailManager _emailManager;
        private readonly TokenService _tokenService;
        private readonly HashService _hashService;

        public PasswordController(ILogger<PasswordController> logger, UserManagerDB context, EmailManager emailManager, TokenService tokenService, HashService hashService)
        {
            _logger = logger;
            _context = context;
            _emailManager = emailManager;
            _tokenService = tokenService;
            _hashService = hashService;
        }

        private async Task<string> GenerateToken(List<string> data, int expiresInMinutes)
        {
            return await _tokenService.GenerateTokenAsync(data, expiresInMinutes);
        }

        [HttpPost("/reset/")]
        public async Task<IActionResult> SendResetPassword([FromBody] string email)
        {
            // Find the user with the provided email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            // If the user does not exist, return a 404 Not Found status
            if (existingUser == null)
            {
                return NotFound(new { message = "A user with this email does not exists" });
            }

            // Create a list with the user's ID to be used for generating the token
            List<string> dataForToken = new()
            {
                existingUser.UserId.ToString()
            };

            // Generate a token that expires in 15 minutes
            var token = GenerateToken(dataForToken, 15);

            // Create a dictionary with the reset password link
            Dictionary<string, object> additianalData = new()
            {
                {"ResetPasswordLink", token}
            };

            // Send an email to the user with the reset password link
            await _emailManager.SendEmailAsync("PasswordReset", "Link for reset password", email, additianalData);

            return Ok(new { message = "Your link for password resetting expires after 15 minutes" });
        }

        [HttpPost("/reset/verify/")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] string newPassword)
        {
            // Verify the token
            var data = await _tokenService.VerifyTokenAsync(token);

            // If the token is invalid, return a 400 Bad Request status with a custom message
            if (data == null)
            {
                return BadRequest(new { invalidToken = true, message = "This token is invalid" });
            }

            // Find the user with the ID from the token
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == int.Parse(data[0]));

            // If the user does not exist, return a 404 Not Found status
            if (existingUser == null)
            {
                return NotFound(new { message = "A user with this email does not exists" });
            }

            // Hash the new password
            string hashedPassword = await _hashService.HashPassword(newPassword);

            // If the new password is the same as the current password, return a 400 Bad Request status
            var userPassword = existingUser.Password;
            if (userPassword == hashedPassword)
            {
                return BadRequest(new { samePassword = true, message = "You can change your password as the same as your current password" });
            }

            // Update the user's password and the last password change date
            existingUser.Password = hashedPassword;
            existingUser.Settings.SecuritySettings.LastPasswordChange = DateTime.UtcNow;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(new {passwordChanged = true, message = "You have successfully changed your password!"});

        }
    }
}