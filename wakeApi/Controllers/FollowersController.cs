#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using wakeApi.Data;
using wakeApi.Identity;
using wakeApi.Models;
using wakeApi.Dtos;
using AutoMapper;

namespace wakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly WakeContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public FollowersController(WakeContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Followers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowerDto>>> GetFollowers()
        {
            return await _context.Followers.Select(f => ItemToDto(f)).ToListAsync();
        }

        // GET: api/Followers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FollowerDto>> GetFollower(int id)
        {
            var follower = await _context.Followers.FindAsync(id);

            if (follower == null)
            {
                return NotFound();
            }

            return ItemToDto(follower);
        }

        // PUT: api/Followers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateFollower/{id}")]
        public async Task<IActionResult> UpdateFollower(int id, FollowerDto followerDto)
        {
            if (id != followerDto.Id)
            {
                return BadRequest();
            }
            var follower = await _context.Followers.FindAsync(id);

            if (follower == null)
            {
                return NotFound();
            }

            _context.Entry(follower).State = EntityState.Modified;

            follower.Id = followerDto.Id;
            follower.FollowerName = followerDto.FollowerName;
            follower.UserId = followerDto.UserId;

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
        public async Task<ActionResult> PostFollower(FollowerDto followerDto)
        {
            var follower = _mapper.Map<Follower>(followerDto);
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
                FollowerName = follower.FollowerName,
                UserId = follower.UserId,
                
            };
        
    }
}
