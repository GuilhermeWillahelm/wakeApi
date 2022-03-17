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
using wakeApi.Dtos;
using wakeApi.Identity;
using wakeApi.Models;
using AutoMapper;

namespace wakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly WakeContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public LikesController(WakeContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetLikes()
        {
            return await _context.Likes.Select(l => ItemToDto(l)).ToListAsync();
        }

        // GET: api/Likes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LikeDto>> GetLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);

            if (like == null)
            {
                return NotFound();
            }

            return ItemToDto(like);
        }

        // PUT: api/Likes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, LikeDto likeDto)
        {
            if (id != likeDto.Id)
            {
                return BadRequest();
            }
            var like = await _context.Likes.FindAsync(id);

            if(like == null)
            {
                NoContent();
            }

            _context.Entry(like).State = EntityState.Modified;
            like.Id = id;
            like.CountLike = likeDto.CountLike;
            like.CountDislike = likeDto.CountDislike;
            like.PostId = likeDto.PostId;
            like.UserId = likeDto.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LikeDto>> PostLike(LikeDto likeDto)
        {
            var like = _mapper.Map<Like>(likeDto);
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await _context.Likes.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }

        private static LikeDto ItemToDto(Like like) =>
            new LikeDto
            {
                Id = like.Id,
                CountLike = like.CountLike,
                CountDislike = like.CountDislike,
                PostId = like.PostId,
                UserId = like.UserId
            };
    }
}
