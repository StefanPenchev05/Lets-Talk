using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Data;

namespace Server.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private ILogger<LoginController> logger;
        private readonly UserManagerDB context;


        public LoginController(ILogger<LoginController> logger, UserManagerDB context)
        {
            this.logger = logger;
            this.context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}