using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
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

        [HttpPost("users")]
        public async IAsyncEnumerable<object> FindUser([FromBody] Search model)
        {
            // Check if the userName is null or empty
            if (string.IsNullOrEmpty(model.userName))
            {
                /// If userName is null or empty, yield break to end the iteration
                yield break;
            }

            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Query the Users table in the database
            var users = _context.Users
                // Filter the users where the UserName contains the provided userName
                .Where(u => u.UserName.Contains(model.userName))
                // Include the Friendships navigation property in the query
                .Include(u => u.Friendships)
                // Skip the users for the previous pages
                .Skip(pageSize * model.pageIndex)
                // Take only the users for the current page
                .Take(pageSize)
                // Select only the FirstName, LastName, and ProfilePictureURL properties
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.ProfilePictureURL,
                    isFriend = u.Friendships.Any(f => f.User2Id == userId || f.User1Id == userId)
                });

            // Iterate over the users
            await foreach (var user in users.AsAsyncEnumerable())
            {
                // Yield return the user to provide it to the enumerator object
                yield return user;
            }
        }

    }
}