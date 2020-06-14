using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fiszki.Repositories;
using System.Security.Claims;

namespace Fiszki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogsController : ControllerBase
    {
        [Authorize]       // GET: api/UserLogs                     
        [HttpGet]
        public List<UserLogs> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var Id = identity.FindFirst(ClaimTypes.Name).Value; 
            int id = Int32.Parse(Id);
            if (identity == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return UserLogsRepository.GetUserLogsList(id);
        }
    }
}
