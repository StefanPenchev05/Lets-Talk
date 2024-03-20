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
using Server.ViewModels;
using Server.Services;
using Server.Models;

namespace Server.Controllers
{
    // Define the route for this controller
    [Route("/auth/login")]
    public class LoginController : Controller
    {
        private ILogger<LoginController> logger;
        private IConfiguration configuration;
        private readonly UserManagerDB context;
        private readonly HashService hashService;
        private readonly EmailManager emailManager;
        private readonly IViewRenderService viewRenderService;


        // Inject dependencies into the constructor
        public LoginController(ILogger<LoginController> logger, UserManagerDB context, EmailManager emailManager, IConfiguration configuration, IViewRenderService viewRenderService)
        {
            this.logger = logger;
            this.context = context;
            this.emailManager = emailManager;
            this.configuration = configuration;
            this.viewRenderService = viewRenderService;
        }

        // Define the Login action
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                // Check if the model is valid
                if (ModelState.IsValid)
                {
                    User user = null;
                    var validator = new LoginValidator();

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
                            var userSettings = user.Settings.SecuritySettings;
                            if (userSettings.TwoFactorAuth)
                            {
                                // Generate a random 5-digit number for the authentication code
                                int authCode = new Random().Next(10000, 100000);

                                // Create a dictionary to hold additional data for the email
                                Dictionary<string, object> additianalData = new()
                                {
                                    {"CodeForAuth", authCode}
                                };

                                // Create a new instance of the EmailManager service
                                var emailService = new EmailManager(configuration, viewRenderService);

                                // Use the EmailManager to send an email with the authentication code and return to the client to check his email and write the code given
                                await emailService.SendEmailAsync("TwoFactorAuth", "Two Factor Authentication Code", model, additianalData);

                                userSettings.TwoFactorAuthCode = authCode;

                                return Ok(new { twoFactorAwait = true, message = "Email has been send to you with the code" });
                            }
                            else
                            {
                                // Set the session value with the user's ID
                                HttpContext.Session.SetString("UserId", user.UserId.ToString());

                                // Return a 200 OK response
                                return Ok(new { message = "Login successful" });
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
                        return StatusCode(404, new { message = "The user is not found!" });
                    }
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
                logger.LogError(ex, "An error occurred while processing the login request.");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

        // Define the Error action
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}