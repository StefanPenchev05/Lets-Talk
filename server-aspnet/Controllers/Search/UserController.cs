using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Server.Data;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {

        private readonly UserManagerDB _context;
        private readonly int pageSize = 10;

        public SearchController(UserManagerDB context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public async Task<IActionResult> FindUser([FromQuery] string userName, [FromQuery] int pageIndex)
        {
            // Check if the userName is null or empty
            if (string.IsNullOrEmpty(userName))
            {
                /// If userName is null or empty, yield break to end the iteration
                return NotFound(new { emptyUser = true });
            }

            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Query the Users table in the database
            var users = await _context.Users
                // Filter the users where the UserName contains the provided userName
                .Where(u => u.UserName.Contains(userName.ToLower()) && u.UserId != userId)
                // Include the Friendships navigation property in the query
                .Include(u => u.Friendships)
                .ThenInclude(f => f.Friend)
                .Include(u => u.FriendOf)
                .ThenInclude(f => f.User)
                // Skip the users for the previous pages
                .Skip(pageSize * pageIndex)
                // Take only the users for the current page
                .Take(pageSize)
                // Select only the FirstName, LastName, and ProfilePictureURL properties
                .Select(u => new
                {
                    username = u.UserName,
                    u.FirstName,
                    u.LastName,
                    avatarURL = u.ProfilePictureURL != null ? $"{Request.Scheme}://{Request.Host}/uploads/{u.UserId}/{Path.GetFileName(u.ProfilePictureURL)}" : null,
                    isFriend = u.Friendships.Any(f => f.FriendId == userId) || u.FriendOf.Any(f => f.UserId == userId)
                })
                .ToListAsync();

            if (!users.Any())
            {
                return NotFound(new { emptyUserBox = true });
            }

            return Ok(users);
        }
    }
}