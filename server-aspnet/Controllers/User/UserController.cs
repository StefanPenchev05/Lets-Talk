using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Data;

namespace Server.Controllers
{
    [Route("/user/")]
    public class UserProfileController : Controller
    {
        private readonly ILogger<UserProfileController> _logger;
        private readonly UserManagerDB _context;

        public UserProfileController(ILogger<UserProfileController> logger, UserManagerDB context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            // Get the user ID from the session
            string userId = HttpContext.Session.GetString("UserId");

            // Try to parse the user ID as an integer
            int id;
            if (!int.TryParse(userId, out id))
            {
                // If the user ID is not a valid integer, return a 400 Bad Request response with an error message
                return BadRequest("Invalid user ID.");
            }

            // Try to find the user in the database
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // If the user is not found, return a 404 Not Found response
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Get the file name from the profile picture URL
            var fileName = Path.GetFileName(existingUser.ProfilePictureURL);

            // Check if the file name is valid
            if (string.IsNullOrEmpty(fileName))
            {
                // If the file name is not valid, return a 400 Bad Request response with an error message
                return BadRequest("Profile picture URL is invalid.");
            }

            // Construct the absolute URL to the profile picture
            var absoluteUrl = $"{Request.Scheme}://{Request.Host}/uploads/{existingUser.UserId}/{fileName}";

            return Ok(new { firstName = existingUser.FirstName, lastName = existingUser.LastName, username = existingUser.UserName, avatar = absoluteUrl });
        }

        [HttpGet("settings")]
        public async Task<IActionResult> GetUserSettings()
        {
            // Get the user ID from the session
            string userId = HttpContext.Session.GetString("UserId");

            // Try to parse the user ID as an integer
            int id;
            if (!int.TryParse(userId, out id))
            {
                // If the user ID is not a valid integer, return a 400 Bad Request response with an error message
                return BadRequest(new { message = "Invalid session" });
            }

            // Retrieve the user settings from the database, including related data
            var userSettings = await _context.Settings
                .Include(p => p.PrivacySettings) // Include the related PrivacySettings
                .Include(s => s.SecuritySettings) // Include the related SecuritySettings
                .Include(p => p.PreferenceSettings) // Include the related PreferenceSettings
                .Include(n => n.NotificationSettings) // Include the related NotificationSettings
                .Include(l => l.LoginLocations) // Include the related LoginLocations
                .FirstOrDefaultAsync(s => s.UserId == id); // Filter by the user ID

            // If the user settings are not found, return a 404 Not Found response with an error message
            if (userSettings == null)
            {
                return NotFound(new { message = "User settings not found" });
            }

            return Ok(userSettings);
        }

        [HttpGet("channels")]
        public async Task<IActionResult> GetUserChannels([FromBody] int index)
        {
            // Get the user ID from the session
            string userId = HttpContext.Session.GetString("UserId");

            // Try to parse the user ID as an integer
            int id;
            if (!int.TryParse(userId, out id))
            {
                // If the user ID is not a valid integer, return a 400 Bad Request response with an error message
                return BadRequest(new { message = "Invalid session" });
            }

            // Define the page size 
            int pageSize = 10;

            // Retrieve the user channels from the database, including related data
            var userChannels = await _context.UserChannels
                .Where(uc => uc.UserId == id) // Filter by the user ID
                .Select(uc => uc.Channel) // Select the related Channel
                .Skip(index * pageSize) // Skip the previous pages
                .Take(pageSize) // Take only the current page
                .ToListAsync();

            // If the user channels are not found, return a 404 Not Found response with an error message
            if (!userChannels.Any())
            {
                return NotFound(new {channelEmpty = true, message = "User channels not found" });
            }

            return Ok(userChannels);
        }
    }
}