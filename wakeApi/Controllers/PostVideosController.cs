using wakeApi.Data;
using wakeApi.Models;
using wakeApi.Dtos;
using wakeApi.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace wakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostVideosController : ControllerBase
    {
        private readonly WakeContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public PostVideosController(WakeContext context, SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostVideoDto>>> GetPostVideos()
        {
            return await _context.PostVideos.Select(x => ItemToDTO(x)).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PostVideo>> GetPostVideo(int id)
        {
            var postItem = await _context.PostVideos.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            return Ok(postItem);
        }

        [HttpPut("UpdatePostVideo/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdatePostVideo(int id, PostVideoDto postVideoDto)
        {
            if (id != postVideoDto.Id)
            {
                return BadRequest();
            }

            var postItem = await _context.PostVideos.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            postItem.Id = id;
            postItem.Title = postVideoDto.Title;
            postItem.Description = postVideoDto.Description;
            postItem.Video = postVideoDto.Video;
            postItem.ThumbImage = postVideoDto.ThumbImage;
            postItem.UserId = postVideoDto.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PostVideosExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("CreatePostVideo")]
        [AllowAnonymous]
        public async Task<ActionResult> CreatePostVideo(PostVideoDto postVideoDto)
        {
            var post = _mapper.Map<PostVideo>(postVideoDto);
            _context.PostVideos.Add(post);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostVideo), new { id = post.Id }, post);

        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeletePostVideo(int id)
        {
            var postItem = await _context.PostVideos.FindAsync(id);

            if (postItem == null)
            {
                return NotFound();
            }

            _context.PostVideos.Remove(postItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostVideosExists(long id)
        {
            return _context.PostVideos.Any(x => x.Id == id);
        }

        private static PostVideoDto ItemToDTO(PostVideo todoItem) =>
            new PostVideoDto
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                Posted = todoItem.Posted,
                Video  = todoItem.Video,
                ThumbImage = todoItem.ThumbImage,
                UserId = todoItem.UserId,
                
            };


    }
}
