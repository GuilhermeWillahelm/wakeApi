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
using wakeApi.Dtos;
using wakeApi.Models;
using AutoMapper;

namespace wakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly WakeContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ChannelsController(WakeContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Channels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChannelDto>>> GetChannels()
        {
            return await _context.Channels.Select(c => ItemToDto(c)).ToListAsync();
        }

        // GET: api/Channels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelDto>> GetChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);

            if (channel == null)
            {
                return NotFound();
            }

            return ItemToDto(channel);
        }

        // PUT: api/Channels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateChannel/{id}")]
        public async Task<IActionResult> UpdateChannel(int id, ChannelDto channelDto)
        {
            if (id != channelDto.Id)
            {
                return BadRequest();
            }
            var channel = await _context.Channels.FindAsync(id);

            if(channel == null)
            {
                NotFound();
            }

            _context.Entry(channel).State = EntityState.Modified;
            channel.Id = id;
            channel.ChannelName = channelDto.ChannelDescription;
            channel.ChannelDescription = channelDto.ChannelDescription;
            channel.CreatedChanel = channelDto.CreatedChanel;
            channel.ImageBanner = channelDto.ImageBanner;
            channel.UserId = channelDto.UserId;
            channel.FollwerId = channelDto.FollwerId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
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

        // POST: api/Channels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChannelDto>> PostChannel(ChannelDto channelDto)
        {
            var channel = _mapper.Map<Channel>(channelDto);
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChannel", new { id = channel.Id }, channel);
        }

        // DELETE: api/Channels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.Id == id);
        }

        private static ChannelDto ItemToDto(Channel channel) =>
            new ChannelDto 
            {
                Id = channel.Id,
                ChannelName =  channel.ChannelName,
                ChannelDescription = channel.ChannelDescription,
                CreatedChanel = channel.CreatedChanel,
                ImageBanner = channel.ImageBanner,
                UserId = channel.UserId,
                FollwerId = channel.FollwerId,
            };
    }
}
