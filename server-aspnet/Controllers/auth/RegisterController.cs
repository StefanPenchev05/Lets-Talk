using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Data;
using Server.ViewModels;
using Server.Models;
using Microsoft.EntityFrameworkCore;
using Server.Interface;

namespace Server.Controllers
{
    [Route("/auth/register")]
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly UserManagerDB _context;
        private readonly IHashService _hashService;

        public RegisterController(ILogger<RegisterController> logger, UserManagerDB context)
        {
            _logger = logger;
            _context = context;
        }

        private async Task<string> SuggestUsername(string username)
        {
            bool isUsernameTaken = true;
            int suffix = 1;
            string suggestedUsername = username;
            while (isUsernameTaken)
            {
                User userWithUsername = await _context.Users
                    .SingleOrDefaultAsync(u => u.UserName == suggestedUsername);

                if (userWithUsername != null)
                {
                    suggestedUsername = $"{username}{suffix}";
                    suffix++;
                }
                else
                {
                    isUsernameTaken = false;
                }
            }

            return suggestedUsername;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the email is already taken
                    User userWithEmail = await _context.Users
                        .SingleOrDefaultAsync(u => u.Email == model.Email);

                    if (userWithEmail != null)
                    {
                        return BadRequest(new { emailExists = true, message = "This email is already taken" });
                    }

                    // Check if the username is already taken
                    User userWithUsername = await _context.Users
                        .SingleOrDefaultAsync(u => u.UserName == model.Username);

                    if (userWithUsername != null)
                    {
                        string suggestedUsername = await SuggestUsername(model.Username);
                        return BadRequest(new { usernameExists = true, message = "This username is already taken", SuggestUsername = suggestedUsername });
                    }

                    string hashedPassword = await _hashService.HashPassword(model.Password);

                    User newUser = new()
                    {
                        Email = model.Email,
                        Password = hashedPassword,
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        ProfilePicture = model.ProfilePicture,
                        Settings = new()
                        {
                            SecuritySettings = new()
                            {
                                TwoFactorAuth = model.TwoFactorAuth != null ? true : false
                            }
                        }
                    };

                    
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