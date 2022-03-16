#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wakeApi.Data;
using wakeApi.Models;

namespace wakeApi.Controllers
{
    public class PostVideosController : Controller
    {
        private readonly WakeContext _context;

        public PostVideosController(WakeContext context)
        {
            _context = context;
        }

        // GET: PostVideos
        public async Task<IActionResult> Index()
        {
            var wakeContext = _context.PostVideos.Include(p => p.User);
            return View(await wakeContext.ToListAsync());
        }

        // GET: PostVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postVideo = await _context.PostVideos
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postVideo == null)
            {
                return NotFound();
            }

            return View(postVideo);
        }

        // GET: PostVideos/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PostVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Posted,Video,UserId")] PostVideo postVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postVideo.UserId);
            return View(postVideo);
        }

        // GET: PostVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postVideo = await _context.PostVideos.FindAsync(id);
            if (postVideo == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postVideo.UserId);
            return View(postVideo);
        }

        // POST: PostVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Posted,Video,UserId")] PostVideo postVideo)
        {
            if (id != postVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostVideoExists(postVideo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", postVideo.UserId);
            return View(postVideo);
        }

        // GET: PostVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postVideo = await _context.PostVideos
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postVideo == null)
            {
                return NotFound();
            }

            return View(postVideo);
        }

        // POST: PostVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postVideo = await _context.PostVideos.FindAsync(id);
            _context.PostVideos.Remove(postVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostVideoExists(int id)
        {
            return _context.PostVideos.Any(e => e.Id == id);
        }
    }
}
