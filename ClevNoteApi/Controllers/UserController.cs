using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fiszki.Repositories;
using Fiszki.Services;
using Fiszki.VievModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;

namespace Fiszki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // POST: api/User
        [HttpPost("login")]
        public IActionResult Login([FromBody] CredentialsModel value)
        {
            try
            {
                var user = UserRepository.Login(value);
                if (user == null)
                {
                    return BadRequest("Nieprawidłowy login lub hasło");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }

        // === Account info ===
        [Authorize]
        [HttpGet("account")]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.FindFirst(ClaimTypes.Name).Value;
            
            if (identity == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var user = UserRepository.Account(id);

            return Ok(new { user.login, user.dateAdd });
        }


        [HttpPost("Register")] 
        public IActionResult Register([FromBody] CredentialsModel value)
        {
            try
            {
                var registerResult = UserRepository.Register(value);
                if (registerResult.status != "ok")
                {
                    return BadRequest(registerResult.coment);
                }
                //return Ok(registerResult.coment);
                return Ok(registerResult.token);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }


    }
}
