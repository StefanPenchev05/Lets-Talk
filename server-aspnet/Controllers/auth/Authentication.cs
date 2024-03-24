using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Data;

namespace Server.Controllers
{
    [Route("/auth")]
    public class AuthenticationSession : Controller
    {
        private readonly ILogger<AuthenticationSession> _logger;

        private readonly UserManagerDB _context;
        public AuthenticationSession(ILogger<AuthenticationSession> logger, UserManagerDB userManagerDB)
        {
            _logger = logger;
            _context = userManagerDB;
        }

        [HttpGet]
        public async Task<IActionResult> AuthSession()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var twoFactorAwait = HttpContext.Session.GetString("TwoFactorAuthenticationID");

            if (userId != null)
            {
                return Ok(new { authSession = true, message = "Session is valid" });
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