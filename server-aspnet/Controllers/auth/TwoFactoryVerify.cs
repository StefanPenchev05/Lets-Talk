using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Models.ViewModels;

namespace Server.Controllers
{
    [Route("/auth/login/verify")]
    public class TwoFactoryVerify : Controller
    {
        private readonly ILogger<TwoFactoryVerify> _logger;

        public TwoFactoryVerify(ILogger<TwoFactoryVerify> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        // public Task<IActionResult> Verify([FromBody] LoginViewModel model)
        // {

        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}