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

            if (ModelState.IsValid)
            {
                if (model.Users == null || !model.Users.Any() || model.Users.Count() < 2)
                {
                    return BadRequest(new { message = "Sorry, insufficient numbers of users" });
                }

                var ownerId = HttpContext.Session.GetString("UserId");

                if (ownerId == null)
                {
                    return BadRequest(new { message = "Session is not valid" });
                }

                var existingOwner = await _context.Users.FirstOrDefaultAsync(u => u.UserId == int.Parse(ownerId));

                model.Users.Add($"{existingOwner.UserName}", "Owner");

                if (model.ChannelImg.Length == 0)
                {
                    return BadRequest(new { message = "Image is required" });
                }

                Channel channel = new()
                {
                    Name = model.ChannelName,
                };

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

                channel.ImageURL = filePath;

                _context.Channels.Add(channel);
                await _context.SaveChangesAsync();

                var keys = model.Users.Keys.ToList();
                foreach (var key in keys)
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == key);
                    if (existingUser == null)
                    {
                        model.Users.Remove(key);
                        continue;
                    }

                    RoleType roleType;
                    if (!Enum.TryParse(model.Users[key], out roleType))
                    {
                        roleType = RoleType.User;
                    }

                    Role role = new()
                    {
                        RoleType = roleType
                    };

                    _context.Roles.Add(role);
                    await _context.SaveChangesAsync();

                    UserChannel userChannel = new()
                    {
                        UserId = existingUser.UserId,
                        ChannelId = channel.ChannelId,
                        RoleId = role.RoleId
                    };

                    _context.UserChannels.Add(userChannel);
                    await _context.SaveChangesAsync();
                }
                return Ok(new { message = "Create Successful" });
            }
            else
            {
                return BadRequest(new { message = "Invalid Model" });
            }
        }

    }
}