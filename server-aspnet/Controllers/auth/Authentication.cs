using Microsoft.AspNetCore.Mvc;
using Server.Data;


namespace Server.Controllers
{
    [Route("/auth")]
    public class AuthenticationSession : Controller
    {
        private readonly ILogger<AuthenticationSession> _logger;
        private readonly UserManagerDB _context;

        public AuthenticationSession(ILogger<AuthenticationSession> logger, UserManagerDB context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AuthSession()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var twoFactorAwait = HttpContext.Session.GetString("TwoFactorAuthenticationID");
            var awaitForEmailVerificationRoomId = Request.Cookies["AwaitForEmailVerification"];

            var existingUser = _context.Users.SingleOrDefault(u => u.UserId.ToString() == userId);
            if (existingUser != null)
            {
                return Ok(new { authSession = true, message = "Session is valid" });
            }
            else if (awaitForEmailVerificationRoomId != null)
            {
                return Ok(new { awaitForEmailVerification = true, roomId = awaitForEmailVerificationRoomId });
            }
            else if (twoFactorAwait != null)
            {
                return Ok(new { awaitTwoFactorAuth = true, message = "Session is valid" });
            }
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("TwoFactorAuthenticationID");

            return BadRequest(new { authSession = false, message = "Session is not valid" });
        }
    }
}