using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodTrackerServices.Models;


namespace PodTrackerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowedPodcastsController : ControllerBase
    {
        private readonly PodTrackdbContext _context;

        public FollowedPodcastsController(PodTrackdbContext context)
        {
            _context = context;
        }

        [HttpGet("favorite")]
        public IActionResult GetFollowedPodcast()
        {

            var userID = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var followedPodcast = _context.FollowedPodcast.Where(x => x.Followed  && x.UserId == userID)
                .OrderBy(x=>x.Title).ToList();
            return Ok(followedPodcast);
        }

        [HttpGet("recentlyPlayed")]
        public IActionResult GetRecentryPlayedPodcast()
        {
            var userID = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var followedPodcast = _context.FollowedPodcast.Where(x => x.LastListened!=null && x.UserId == userID)
                .OrderByDescending(x => x.LastListened).Take(20).ToList();
            return Ok(followedPodcast);
        }

        // GET: api/FollowedPodcasts/5
        [HttpGet]
        public async Task<IActionResult> GetFollowedPodcast([FromQuery] string rss)
        {

            var userID = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FollowedPodcast followedPodcast;
           
                followedPodcast = await _context.FollowedPodcast.Include(x=>x.PodcastEpisode)
                        .FirstOrDefaultAsync(x => x.Rss == rss && x.UserId == userID);
           

            if (followedPodcast == null)
            {
                return NotFound();
            }

            return Ok(followedPodcast);
        }

        // PUT: api/FollowedPodcasts/5
        [HttpPut]
        public async Task<IActionResult> PutFollowedPodcast([FromQuery] string rss, [FromBody] FollowedPodcast followedPodcast)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (rss != followedPodcast.Rss)
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
                if (!FollowedPodcastExists(rss))
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

            return CreatedAtAction("GetFollowedPodcast", new { rss = followedPodcast.Rss }, followedPodcast);
        }

        // DELETE: api/FollowedPodcasts/5
        [HttpDelete]
        public async Task<IActionResult> DeleteFollowedPodcast([FromQuery] string rss)
        {

            var userID = Convert.ToInt32(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followedPodcast = await _context.FollowedPodcast
                .FirstOrDefaultAsync(x => x.Rss == rss && x.UserId == userID);
            if (followedPodcast == null)
            {
                return NotFound();
            }

            _context.FollowedPodcast.Remove(followedPodcast);
            await _context.SaveChangesAsync();

            return Ok(followedPodcast);
        }

        private bool FollowedPodcastExists(string rss)
        {
            return _context.FollowedPodcast.Any(e => e.Rss == rss);
        }
    }
}