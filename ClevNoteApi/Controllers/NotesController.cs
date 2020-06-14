using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiszki.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fiszki.VievModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Fiszki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        // GET: api/Notes
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] string itemType, int userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = identity.FindFirst(ClaimTypes.Name).Value;
            int ID = Int32.Parse(id);
            try
            {
                return Ok(NotesRepository.Get(itemType, ID));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }

        // GET: api/Notes/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userid = identity.FindFirst(ClaimTypes.Name).Value;
            int Userid = Int32.Parse(userid);
            try
            {
                var note = NotesRepository.GetNotesId(id, Userid);
                if (!note.Any())
                {
                    return NotFound();
                }
                return Ok(note);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }



        // POST: api/Notes
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] NotesModel NewNote)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userid = identity.FindFirst(ClaimTypes.Name).Value;
            int Userid = Int32.Parse(userid);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                return Ok(NotesRepository.AddNote(NewNote, Userid));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }


        // PUT: api/Notes/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] NotesModel NoteEdited)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userid = identity.FindFirst(ClaimTypes.Name).Value;
            int Userid = Int32.Parse(userid);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(NotesRepository.EditNote(id, NoteEdited, Userid));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed: {ex}");
            }
        }


        // DELETE: api/ApiWithActions/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userid = identity.FindFirst(ClaimTypes.Name).Value;
            int Userid = Int32.Parse(userid);

            var deleteID = NotesRepository.DeleteNote(id, Userid);
            if (deleteID == 0)
            {
                return NotFound($"Not found item id: {id}");
            }
            return Ok( $"Deleted item id: {deleteID}" );
        }
    }
}
