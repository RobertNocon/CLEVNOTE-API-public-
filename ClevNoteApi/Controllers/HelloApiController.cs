using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiszki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloApiController : ControllerBase
    {
        // GET: api/HelloApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "ClevNotes", "Robert Nocoń", "v3" };
        }
    }
}
