using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Services;
using Server.ViewModels;

namespace Server.Controllers
{
    [Route("/auth/login/verify")]
    public class TwoFactoryVerify : Controller
    {
        private readonly ILogger<TwoFactoryVerify> _logger;
        private readonly UserManagerDB _context;
        private ICryptoService _cryptoService;

        public TwoFactoryVerify(ILogger<TwoFactoryVerify> logger, UserManagerDB context)
        {
            _logger = logger;
            _context = context;
        }

        // Method to decrypt the user ID
        private async Task<string> DecryptUserId(byte[] encryptedUserId)
        {
            return await _cryptoService.DecryptAsync(encryptedUserId);
        }

        // POST method to verify the two-factor authentication
        [HttpPost]
        public async Task<IActionResult> Verify([FromBody] TwoFactorVerifyViewModel model)
        {
            // Get the encrypted user ID from the session
            string encryptedUserId = HttpContext.Session.GetString("ID");
            // Convert the encrypted user ID to a byte array
            byte[] encryptedUserIdBytes = Encoding.UTF8.GetBytes(encryptedUserId);

            // Decrypt the user ID
            string decryptedUserId = await DecryptUserId(encryptedUserIdBytes);

            // Get the user from the database
            User user = await _context.Users
                .Include(u => u.Settings)
                .ThenInclude(s => s.SecuritySettings)
                .SingleOrDefaultAsync(u => u.UserId == int.Parse(decryptedUserId));
            
            // If the user is not found, return an error
            if(user == null)
            {
                return NotFound(new {message = "Invalid session"});
            }

            // If the two-factor authentication code is incorrect, return an error
            if(user.Settings.SecuritySettings.TwoFactorAuthCode != model.TwoFactorAuthCode)
            {
                return BadRequest(new {validCode = false, message = "Incorrect Code"});
            }

            // If the two-factor authentication code is correct, update the session and clear the code
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            user.Settings.SecuritySettings.TwoFactorAuthCode = null;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Return a success message
            return Ok(new {validCode = true, message = "Successful verified", greetings = $"Welcome back {user.UserName}"});
        }
    }
}