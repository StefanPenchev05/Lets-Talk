using System.IO;
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
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterController(ILogger<RegisterController> logger, UserManagerDB context, IHashService hashService, ITokenService tokenService, IEmailService emailService, ICryptoService cryptoService, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hashService = hashService;
            _tokenService = tokenService;
            _emailService = emailService;
            _cryptoService = cryptoService;
            _hostEnvironment = hostEnvironment;
        }

        private async Task<string> SuggestUsername(string username)
        {
            // Initialize a flag to check if the username is taken
            bool isUsernameTaken = true;

            // Initialize a suffix to append to the username if it's taken
            int suffix = 1;

            // Initialize the suggested username with the provided username
            string suggestedUsername = username;

            // Loop until we find a username that's not taken
            while (isUsernameTaken)
            {
                // Try to find a user with the suggested username
                User userWithUsername = await _context.Users
                    .SingleOrDefaultAsync(u => u.UserName == suggestedUsername);

                // If a user with the suggested username exists...
                if (userWithUsername != null)
                {
                    // Append the suffix to the username and increment the suffix
                    suggestedUsername = $"{username}{suffix}";
                    suffix++;
                }
                else
                {
                    // If no user with the suggested username exists, set the flag to false
                    isUsernameTaken = false;
                }
            }

            // Return the suggested username
            return suggestedUsername;
        }

        private async Task<string> GenerateToken(List<string> data, int expiresInMinutes)
        {
            return await _tokenService.GenerateTokenAsync(data, expiresInMinutes);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            Console.WriteLine(model.Email);
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

                    // If a user with the same username already exists...
                    if (userWithUsername != null)
                    {
                        // Generate a suggested username
                        string suggestedUsername = await SuggestUsername(model.Username);

                        // Return a BadRequest response with the error message and the suggested username
                        return BadRequest(new { usernameExists = true, message = "This username is already taken", SuggestUsername = suggestedUsername });
                    }

                    // Hash the password using the hash service
                    string hashedPassword = await _hashService.HashPassword(model.Password);

                    // Generate a new verification code
                    string roomId = Guid.NewGuid().ToString();

                    // Create a new TempData object for the new user
                    TempData tempUser = new()
                    {
                        Email = model.Email,
                        Password = hashedPassword,
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        TwoFactorAuth = model.TwoFactorAuth,
                        VerificationCode = roomId
                    };

                    if (model.ProfilePicture != null)
                    {
                        // Define the path where the user's profile picture will be stored
                        string wwwRoot = _hostEnvironment.WebRootPath;
                        string uploadTempDir = Path.Combine(wwwRoot, "uploadsTemp", tempUser.Id.ToString());

                        // Create the directory if it doesn't exist
                        if (!Directory.Exists(uploadTempDir))
                        {
                            Directory.CreateDirectory(uploadTempDir);
                        }

                        // Define the file name for the profile picture
                        string fileName = Path.GetFileNameWithoutExtension(model.ProfilePicture.FileName);
                        string extension = Path.GetExtension(model.ProfilePicture.FileName);
                        fileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
                        string filePath = Path.Combine(uploadTempDir, fileName);


                        // Save the profile picture to the file system
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProfilePicture.CopyToAsync(stream);
                        }

                        // Set the URL of the profile picture
                        tempUser.ProfilePictureURL = filePath;
                    }

                    // Add the tempUser to the DbSet
                    _context.tempDatas.Add(tempUser);

                    // Save the changes to the database
                    await _context.SaveChangesAsync();

                    // Create a list to hold the data that will be used to generate the token
                    // This includes the room ID and the temporary user's ID
                    List<string> dataForToken = new()
                    {
                        roomId,
                        tempUser.Id.ToString()
                    };

                    // Generate a token using the data and set it to expire in 15 minutes
                    string token = await GenerateToken(dataForToken, 15);

                    // Create a dictionary to hold additional data that will be sent in the email
                    // This includes the verification link, which is the generated token
                    Dictionary<string, object> additionalData = new()
                    {
                        {"VerificationLink", token}
                    };

                    // Send an email to the user with the verification link
                    // The email's subject is "Link For Email Verification" and the body is generated using the "EmailVerification" template
                    await _emailService.SendEmailAsync("EmailVerification", "Link For Email Verification", model.Email, additionalData);

                    // Return a success response indicating that the email has been sent and the user should check their email for the verification link
                    return Ok(new { AwaitForEmailVerification = true, roomId, message = "Sended Email" });
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

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            // Verify the token
            var data = await _tokenService.VerifyTokenAsync(token);

            // If the token is invalid, return a 404 status code with a custom message
            if (data == null)
            {
                return StatusCode(404, new { invalidToken = true, message = "This token is invalid" });
            }

            // Extract the room ID and temporary user ID from the data
            var roomId = data[0];
            var tempUserId = data[1];

            // Try to find the temporary user in the database
            var existingTempUser = await _context.tempDatas.SingleOrDefaultAsync(t => t.Id == int.Parse(tempUserId));

            // If the temporary user is not found, return a 404 status code with a custom message
            if (existingTempUser == null)
            {
                return StatusCode(404, new { tempUserNotFound = true, message = "Your temporary registration data could not be found or has expired. Please register again." });
            }

            // Check if a user with the same email or username already exists
            var exisitngUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == existingTempUser.Email || u.UserName == existingTempUser.UserName);

            // If such a user exists, return a bad request status code with a custom message
            if (exisitngUser != null)
            {
                return BadRequest(new { invalidToken = true, message = "This token has already been used" });
            }

            // Create a new user with the data from the temporary user
            User newUser = new()
            {
                Email = existingTempUser.Email,
                UserName = existingTempUser.UserName,
                FirstName = existingTempUser.FirstName,
                LastName = existingTempUser.LastName,
                Password = existingTempUser.Password,
                Settings = new()
                {
                    SecuritySettings = new()
                    {
                        TwoFactorAuth = existingTempUser.TwoFactorAuth == false ? false : true
                    }
                }
            };

            string wwwRoot = _hostEnvironment.WebRootPath;
            string uploadDir = Path.Combine(wwwRoot, "uploads", newUser.UserId.ToString());

            // Create the directory if it doesn't exist
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Define the source file and the destination file
            string sourceFile = existingTempUser.ProfilePictureURL;
            string destinationFile = Path.Combine(uploadDir, Path.GetFileName(sourceFile));

            // Move File
            System.IO.File.Move(sourceFile, destinationFile);

            newUser.ProfilePictureURL = destinationFile;

            // Add the new user to the database
            _context.Users.Add(newUser);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Notify the user that their email has been verified
            //await _authHub.SendToRoom(roomId);

            // Return a 200 status code
            return Ok();
        }
    }
}