using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodTrackerServices.Models;

namespace PodTrackerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastEpisodesController : ControllerBase
    {
        private readonly PodTrackdbContext _context;

        public PodcastEpisodesController(PodTrackdbContext context)
        {
            _context = context;
        }

        // GET: api/PodcastEpisodes
        [HttpGet]
        public IEnumerable<PodcastEpisode> GetPodcastEpisode()
        {
            return _context.PodcastEpisode;
        }

        // GET: api/PodcastEpisodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodcastEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podcastEpisode = await _context.PodcastEpisode.FindAsync(id);

            if (podcastEpisode == null)
            {
                return NotFound();
            }

            return Ok(podcastEpisode);
        }

        // PUT: api/PodcastEpisodes?id={id}
        [HttpPut]
        public async Task<IActionResult> PutPodcastEpisode([FromQuery] int id, [FromBody] PodcastEpisode podcastEpisode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != podcastEpisode.Id)
            {
                return BadRequest();
            }

            _context.Entry(podcastEpisode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodcastEpisodeExists(id))
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

        // POST: api/PodcastEpisodes
        [HttpPost]
        public async Task<IActionResult> PostPodcastEpisode([FromBody] PodcastEpisode podcastEpisode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PodcastEpisode.Add(podcastEpisode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodcastEpisode", new { id = podcastEpisode.Id }, podcastEpisode);
        }

        // DELETE: api/PodcastEpisodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodcastEpisode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var podcastEpisode = await _context.PodcastEpisode.FindAsync(id);
            if (podcastEpisode == null)
            {
                return NotFound();
            }

            _context.PodcastEpisode.Remove(podcastEpisode);
            await _context.SaveChangesAsync();

            return Ok(podcastEpisode);
        }

        private bool PodcastEpisodeExists(int id)
        {
            return _context.PodcastEpisode.Any(e => e.Id == id);
        }
    }
}