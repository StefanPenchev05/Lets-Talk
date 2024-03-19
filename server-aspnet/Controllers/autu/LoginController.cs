using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Server NameSpaces
using Server.Data;
using Server.Validator;
using Server.Models.ViewModels;
using Server.Services;
using Server.Models;

namespace Server.Controllers
{
    // Define the route for this controller
    [Route("/auth/[controller]")]
    public class LoginController : Controller
    {
        // Define the logger, context, hash service, and validator
        private ILogger<LoginController> logger;
        private readonly UserManagerDB context;
        private readonly HashService hashService;
        private readonly LoginValidator validator;

        // Inject dependencies into the constructor
        public LoginController(ILogger<LoginController> logger, UserManagerDB context)
        {
            this.logger = logger;
            this.context = context;
        }

        // Define the Login action
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                User user = null;
                // Determine if the input is an email or username
                string emailOrUsername = validator.isEmailOrUsername(model.UsernameOrEmail);
                if (emailOrUsername == "email")
                {
                    // If it's an email, find the user with this email
                    user = await context.Users.SingleOrDefaultAsync(u => u.Email == model.UsernameOrEmail);
                }
                else
                {
                    // If it's a username, find the user with this username
                    user = await context.Users.SingleOrDefaultAsync(u => u.UserName == model.UsernameOrEmail);
                }

                // If the user is found
                if (user != null)
                {
                    // Hash the input password
                    var passwordHasher = hashService.HashPassword(model.Password);
                    // If the hashed password matches the user's password
                    if (user.Password == passwordHasher)
                    {
                        // Check if the user has enabled two-factor authentication
                        var userSettings = user.Settings;
                        if(userSettings.TwoFactorAuth){
                            
                        }
                    }
                    else
                    {
                        // If the passwords don't match, return an error
                        return BadRequest(new { message = "Invalid password for this user" });
                    }
                }
                else
                {
                    // If the user is not found, return an error
                    return BadRequest(new { message = "The user is not found!" });
                }
            }

            // If the model is not valid, return a server error
            return BadRequest(new { message = "Server Error" });
        }

        // Define the Error action
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}