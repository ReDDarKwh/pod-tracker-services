using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodTrackerServices.Helpers;
using PodTrackerServices.Models;
using PodTrackerServices.Services;
using static PodTrackerServices.Helpers.PasswordSecurity;

namespace PodTrackerServices.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PodUsersController : ControllerBase
    {


        private IUserService _userService;

        public PodUsersController(IUserService userService)
        {
            _userService = userService;

        }



        // GET: api/PodUsers
        [HttpGet]
        public IEnumerable<PodUser> GetPodUser()
        {
            using (var db = new PodTrackdbContext())
            {

                return db.PodUser;
            }


        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]PodUser userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        //// GET: api/PodUsers/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPodUser([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var podUser = await _context.PodUser.FindAsync(id);

        //    if (podUser == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(podUser);
        //}

        //// PUT: api/PodUsers/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPodUser([FromRoute] int id, [FromBody] PodUser podUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != podUser.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(podUser).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PodUserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/PodUsers
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PodUser podUser)
        {
            using (var db = new PodTrackdbContext())
            {

                if (db.PodUser.FirstOrDefault(x => x.Username == podUser.Username) != null) {
                    return BadRequest(new { message = "Username already exists." });
                }

                podUser.Password = PasswordStorage.CreateHash(podUser.Password);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.PodUser.Add(podUser);
                await db.SaveChangesAsync();

                return Ok();
            }
        }

        //// DELETE: api/PodUsers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePodUser([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var podUser = await _context.PodUser.FindAsync(id);
        //    if (podUser == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PodUser.Remove(podUser);
        //    await _context.SaveChangesAsync();

        //    return Ok(podUser);
        //}

        //private bool PodUserExists(int id)
        //{
        //    return _context.PodUser.Any(e => e.Id == id);
        //}


    }
}