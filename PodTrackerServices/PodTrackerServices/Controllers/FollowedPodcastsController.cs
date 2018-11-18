using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodTrackerServices.podtrackdb;

namespace PodTrackerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowedPodcastsController : ControllerBase
    {
        private readonly podtrackdbContext _context;

        public FollowedPodcastsController(podtrackdbContext context)
        {
            _context = context;
        }

        // GET: api/FollowedPodcasts
        [HttpGet]
        public IEnumerable<FollowedPodcast> GetFollowedPodcast()
        {
            return _context.FollowedPodcast;
        }

        // GET: api/FollowedPodcasts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowedPodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followedPodcast = await _context.FollowedPodcast.FindAsync(id);

            if (followedPodcast == null)
            {
                return NotFound();
            }

            return Ok(followedPodcast);
        }

        // PUT: api/FollowedPodcasts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowedPodcast([FromRoute] int id, [FromBody] FollowedPodcast followedPodcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followedPodcast.Id)
            {
                return BadRequest();
            }

            _context.Entry(followedPodcast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowedPodcastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FollowedPodcasts
        [HttpPost]
        public async Task<IActionResult> PostFollowedPodcast([FromBody] FollowedPodcast followedPodcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FollowedPodcast.Add(followedPodcast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowedPodcast", new { id = followedPodcast.Id }, followedPodcast);
        }

        // DELETE: api/FollowedPodcasts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowedPodcast([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followedPodcast = await _context.FollowedPodcast.FindAsync(id);
            if (followedPodcast == null)
            {
                return NotFound();
            }

            _context.FollowedPodcast.Remove(followedPodcast);
            await _context.SaveChangesAsync();

            return Ok(followedPodcast);
        }

        private bool FollowedPodcastExists(int id)
        {
            return _context.FollowedPodcast.Any(e => e.Id == id);
        }
    }
}