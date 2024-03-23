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
    [Route("/auth/session")]
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
            if (HttpContext.Session.Keys.Contains("Auth"))
            {
                var userId = HttpContext.Session.GetString("Auth");
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == int.Parse(userId));
                
                if (user == null)
                {
                    return BadRequest(new { authSession = false, message = "Session is not valid" });
                }

                return Ok(new { authSession = true, message = "Session is valid" });
            }
            return BadRequest(new { authSession = false, message = "Session is not valid", });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}