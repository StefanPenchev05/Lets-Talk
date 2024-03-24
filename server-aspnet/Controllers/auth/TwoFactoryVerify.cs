using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Interface;
using Server.ViewModels;

namespace Server.Controllers
{
    [Route("/auth/login/verify")]
    public class TwoFactoryVerify : Controller
    {
        private readonly ILogger<TwoFactoryVerify> _logger;
        private readonly UserManagerDB _context;
        private ICryptoService _cryptoService;

        public TwoFactoryVerify(ILogger<TwoFactoryVerify> logger, UserManagerDB context, ICryptoService cryptoService)
        {
            _logger = logger;
            _context = context;
            _cryptoService = cryptoService;
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
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the encrypted user ID from the session
                    string encryptedUserId = HttpContext.Session.GetString("TwoFactorAuthenticationID");
                    // Convert the encrypted user ID to a byte array
                    byte[] encryptedUserIdBytes = Encoding.UTF8.GetBytes(encryptedUserId);

                    // Decrypt the user ID
                    string decryptedUserId = await DecryptUserId(encryptedUserIdBytes);

                    // Get the user from the database
                    User user = await _context.Users
                        .Include(u => u.Settings)
                        .ThenInclude(s => s.SecuritySettings)
                        .SingleOrDefaultAsync(u => u.UserId == int.Parse(decryptedUserId));

                    // If the two-factor authentication code is incorrect, return an error
                    if (user.Settings.SecuritySettings.TwoFactorAuthCode != model.TwoFactorAuthCode)
                    {
                        return BadRequest(new { validCode = false, message = "Incorrect Code" });
                    }

                    // If the two-factor authentication code is correct, update the session and clear the code
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    user.Settings.SecuritySettings.TwoFactorAuthCode = null;

                    // Save the changes to the database
                    await _context.SaveChangesAsync();

                    // Return a success message and delete the session from the user
                    Response.Cookies.Delete("TwoFactorAuthenticationID");
                    return Ok(new { validCode = true, message = "Successful verified", greetings = $"Welcome back {user.UserName}" });
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