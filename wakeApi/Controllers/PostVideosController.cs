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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Hosting;

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
        public static IWebHostEnvironment _env;

        public PostVideosController(WakeContext context, SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _env = env;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostVideoDto>>> GetPostVideos(string? searchString)
        {
            var postVideo = from p in _context.PostVideos select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                //posts = posts.Where(s => s.Title!.Contains(searchString) || s.Description.Contains(searchString));
                postVideo = postVideo.Where(x => x.Title!.Contains(searchString) || x.Description.Contains(searchString));
            }

            return await postVideo.Select(x => ItemToDTO(x)).ToListAsync();
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

        [HttpGet("GetPostVideoById/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PostVideoDto>>> GetPostVideoById(int id)
        {
            var postVideo = from p in _context.PostVideos select p;
            postVideo = postVideo.Where(p => p.UserId == id);

            if (postVideo == null)
            {
                return NotFound();
            }

            return await postVideo.Select(x => ItemToDTO(x)).ToListAsync();
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
            postItem.VideoFile = postVideoDto.VideoFile;
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
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Posted = todoItem.Posted,
                VideoFile = todoItem.VideoFile,
                ThumbImage = todoItem.ThumbImage,
                UserId = todoItem.UserId,

            };
    }
}
