using Server.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Services;
using Microsoft.IdentityModel.Tokens;
using Server.ViewModels;

namespace Server.Controllers
{
    [Route("password")]
    public class PasswordController : Controller
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly UserManagerDB _context;
        private readonly IEmailService _emailManager;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;

        public PasswordController(ILogger<PasswordController> logger, UserManagerDB context, IEmailService emailManager, ITokenService tokenService, IHashService hashService)
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

        [HttpPost("reset")]
        public async Task<IActionResult> SendResetPassword([FromBody] string emailOrUsername)
        {
            // Find the user with the provided email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailOrUsername || u.UserName == emailOrUsername);

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
            var token = await GenerateToken(dataForToken, 15);

            // Create a dictionary with the reset password link
            Dictionary<string, object> additianalData = new()
            {
                {"ResetPasswordLink", token}
            };

            // Send an email to the user with the reset password link
            await _emailManager.SendEmailAsync("PasswordReset", "Link for reset password", existingUser.Email, additianalData);

            return Ok(new { message = "Your link for password resetting expires after 15 minutes" });
        }

        [HttpGet("reset/token")]
        public async Task<IActionResult> CheckToken([FromQuery] string token)
        {
            var validToken = await _tokenService.VerifyTokenAsync(token);

            if (validToken == null)
            {
                return NotFound(new { invalidToken = true });
            }

            return Ok();
        }

        [HttpPost("reset/verify")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPassword model)
        {
            if (_context == null || _tokenService == null || _hashService == null)
            {
                throw new InvalidOperationException("Services are not properly initialized");
            }
            if (token == null)
            {
                return BadRequest(new { invalidToken = true, message = "This token is invalid" });
            }

            // Verify the token
            var data = await _tokenService.VerifyTokenAsync(token);

            // If the token is invalid, return a 400 Bad Request status
            if (data == null)
            {
                return BadRequest(new { invalidToken = true, message = "This token is invalid" });
            }
            
            // if the confirmPassword is empty or null, return a 400 Bad Request status
            if (model.confirmPassword.IsNullOrEmpty())
            {
                return BadRequest(new { emptyPassword = true, message = "Password should not be empty" });
            }


            // Find the user with the ID from the token
            var existingUser = await _context.Users.Include(s => s.Settings.SecuritySettings).FirstOrDefaultAsync(u => u.UserId == int.Parse(data[0]));

            // If the user does not exist, return a 404 Not Found status
            if (existingUser == null)
            {
                return NotFound(new { invalidToken = true, message = "A user with this email does not exists" });
            }

            // Hash the new password
            string hashedPassword = await _hashService.HashPassword(model.confirmPassword);

            // If the new password is the same as the current password, return a 400 Bad Request status
            var userPassword = existingUser.Password;
            if (userPassword == hashedPassword)
            {
                return BadRequest(new { samePassword = true, message = "You cannot change your password as the same as your current password" });
            }

            // Update the user's password and the last password change date
            existingUser.Password = hashedPassword;
            existingUser.Settings.SecuritySettings.LastPasswordChange = DateTime.UtcNow;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { passwordChanged = true, message = "You have successfully changed your password!" });

        }
    }
}