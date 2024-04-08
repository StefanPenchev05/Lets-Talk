using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Data;
using Server.ViewModels;

namespace Server.Controllers
{
    [ApiController]
    [Route("channel")]
    public class CreateController : ControllerBase
    {
        private readonly UserManagerDB _context;
        public CreateController(UserManagerDB context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateViewModel model)
        {   
            foreach(var user in model.Users)
            {
                //var existingUser = await _context.Users
            }

            return Ok();
        }
    }
}