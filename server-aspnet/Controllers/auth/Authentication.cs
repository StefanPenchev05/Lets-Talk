using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [Route("/auth")]
    public class AuthenticationSession : Controller
    {
        private readonly ILogger<AuthenticationSession> _logger;

        public AuthenticationSession(ILogger<AuthenticationSession> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> AuthSession()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var twoFactorAwait = HttpContext.Session.GetString("TwoFactorAuthenticationID");
            var awaitForEmailVerificationRoomId = Request.Cookies["AwaitForEmailVerification"];

            if (userId != null)
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