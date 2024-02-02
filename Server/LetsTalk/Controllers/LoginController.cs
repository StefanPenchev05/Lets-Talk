using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LetsTalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [EnableCors("AllowSpecificOrigin")]
        public IActionResult Login(UserLoginModel userLogin){
            Console.WriteLine(userLogin.EmailOrUsername + " " + userLogin.Password);

            return Ok(userLogin);
        }
    }

    public class UserLoginModel{
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}