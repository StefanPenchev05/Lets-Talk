using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.Models;
using Server.ViewModels;

namespace Server.Controllers
{
    [ApiController]
    [Route("channel")]
    public class CreateController : Controller
    {
        private readonly UserManagerDB _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CreateController(UserManagerDB context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateViewModel model)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Check if the number of users is sufficient
                if (model.Users == null || !model.Users.Any() || model.Users.Count() < 2)
                {
                    return BadRequest(new { message = "Sorry, insufficient numbers of users" });
                }

                // Get the owner ID from the session
                var ownerId = HttpContext.Session.GetString("UserId");

                // Check if the session is valid
                if (ownerId == null)
                {
                    return BadRequest(new { message = "Session is not valid" });
                }

                // Get the existing owner from the database
                var existingOwner = await _context.Users.FirstOrDefaultAsync(u => u.UserId == int.Parse(ownerId));

                // Add the owner to the users
                model.Users.Add($"{existingOwner.UserName}", "Owner");

                // Check if the image is provided
                if (model.ChannelImg.Length == 0)
                {
                    return BadRequest(new { message = "Image is required" });
                }

                // Create a new channel
                Channel channel = new()
                {
                    Name = model.ChannelName,
                };

                // Define the upload directory
                string wwwRoot = _hostEnvironment.WebRootPath;
                string uploadDir = Path.Combine(wwwRoot, "channels", channel.ChannelId.ToString(), "avatar");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Define the file name for the profile picture
                string fileName = Path.GetFileNameWithoutExtension(model.ChannelImg.FileName);
                string extension = Path.GetExtension(model.ChannelImg.FileName);
                fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                string filePath = Path.Combine(uploadDir, fileName);

                // Save the profile picture to the file system
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ChannelImg.CopyToAsync(stream);
                }

                // Set the image URL of the channel
                channel.ImageURL = filePath;

                // Add the channel to the context
                _context.Channels.Add(channel);
                await _context.SaveChangesAsync();

                // Iterate over the users
                var keys = model.Users.Keys.ToList();
                foreach (var key in keys)
                {
                    // Get the existing user from the database
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == key);
                    if (existingUser == null)
                    {
                        // Remove the user if it doesn't exist
                        model.Users.Remove(key);
                        continue;
                    }

                    // Parse the role type
                    RoleType roleType;
                    if (!Enum.TryParse(model.Users[key], out roleType))
                    {
                        roleType = RoleType.User;
                    }

                    // Create a new role
                    Role role = new()
                    {
                        RoleType = roleType
                    };

                    // Add the role to the context
                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();

                    // Create a new user channel
                    UserChannel userChannel = new()
                    {
                        UserId = existingUser.UserId,
                        ChannelId = channel.ChannelId,
                        RoleId = role.RoleId
                    };

                    // Add the user channel to the context
                    _context.UserChannels.Add(userChannel);
                    await _context.SaveChangesAsync();
                }

                // Return a success message
                return Ok(new {roomId = channel.ChannelId, message = "Create Successful" });
            }
            else
            {
                // Return an error message if the model state is not valid
                return BadRequest(new { message = "Invalid Model" });
            }
        }
    }
}