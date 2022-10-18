using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wakeApi.Data;
using wakeApi.Models;
using wakeApi.Dtos;

namespace wakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly WakeContext _context;

        public FollowersController(WakeContext context)
        {
            _context = context;
        }

        // GET: api/Followers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follower>>> GetFollowers()
        {
            return await _context.Followers.ToListAsync();
        }

        [HttpGet("GetFollowersPerChannel/{Idchannel}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FollowerDto>>> GetFollowersPerChannel(int Idchannel)
        {
            return await _context.Followers.Where(f => f.ChannelId == Idchannel).Select(f => ItemToDto(f)).ToListAsync();
        }

        // GET: api/Followers/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Follower>> GetFollower(int id)
        {
            var follower = await _context.Followers.FindAsync(id);

            if (follower == null)
            {
                return NotFound();
            }

            return follower;
        }

        // PUT: api/Followers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollower(int id, Follower follower)
        {
            if (id != follower.Id)
            {
                return BadRequest();
            }

            _context.Entry(follower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowerExists(id))
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

        // POST: api/Followers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Follower>> PostFollower(Follower follower)
        {
            _context.Followers.Add(follower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollower", new { id = follower.Id }, follower);
        }

        // DELETE: api/Followers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollower(int id)
        {
            var follower = await _context.Followers.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }

            _context.Followers.Remove(follower);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowerExists(int id)
        {
            return _context.Followers.Any(e => e.Id == id);
        }

        private static FollowerDto ItemToDto(Follower follower) =>
            new FollowerDto 
            {
                Id = follower.Id,
                CountFollows = follower.CountFollows,
                UserId = follower.UserId,
                ChannelId = follower.ChannelId,
                IsFollowing = follower.IsFollowing
            };
    }
}
