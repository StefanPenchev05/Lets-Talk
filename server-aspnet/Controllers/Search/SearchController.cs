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

        // [HttpPost("/messages")]
        // public async Task<IActionResult> FindMessages([FromBody] Search model)
        // {
        //     if (model.message.IsNullOrEmpty())
        //     {
        //         return Ok(new { emptyMessageBox = true, message = "No messages found" });
        //     }

        //     var userId = HttpContext.Session.GetString("UserId");
        //     if (userId == null)
        //     {
        //         return BadRequest(new { invalidSession = true });
        //     }

        //     int pageSize = 10;

        //     var userChannels = await _context.UserChannels
        //         .Where(u => u.UserId.ToString() == userId)
        //         .Skip(model.pageIndex * pageSize)
        //         .Take(pageSize)
        //         .Select(u => u.ChannelId)
        //         .ToListAsync();

        //     if (userChannels == null)
        //     {
        //         return Ok(new { emptyMessageBox = true, message = "No messages found" });

        //     }

        //     foreach (var channel in userChannels)
        //     {
        //         var 
        //     }


        // }

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

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUserFriendships(int userId)
        {
            var userFriendships = await _context.Friendships
        .Where(f => f.UserId == userId || f.FriendId == userId)
        .Include(f => f.User)
        .Include(f => f.Friend)
        .ToListAsync();

            var friends = new List<User>();

            foreach (var friendship in userFriendships)
            {
                friends.Add(friendship.UserId == userId ? friendship.Friend : friendship.User);
            }

            return Ok(friends);
        }

    }
}