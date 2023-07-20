using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVD_Shop.Data;
using DVD_Shop.Models;

namespace DVD_Shop.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;

        public SongsController(ApplicationDbContext context, IWebHostEnvironment iwc)
        {
            _context = context;
            iw = iwc;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Song.Include(s => s.Album).Include(s => s.Artist).Include(s => s.Genres);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Genres)
                .FirstOrDefaultAsync(m => m.songId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "Title");
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "aName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "gName");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Song song, IFormFile _song)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(song);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            if (_song != null)
            {
                string ext = Path.GetExtension(_song.FileName);
                if (ext == ".mp3" || ext == ".wav" || ext == ".ogg")
                {
                    string directory = Path.Combine(iw.WebRootPath, "Songs");
                    var fileName = Path.GetFileName(_song.FileName);
                    string filePath = Path.Combine(directory, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await _song.CopyToAsync(fileStream);
                    }
                    song.songPath = @"\Songs\" + fileName;
                    _context.Add(song);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Wrong Song Format";
                }
            }

            //ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", song.AlbumId);
            //ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", song.ArtistId);
            //ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", song.GenreId);
            return View();
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", song.GenreId);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Song song)
        {
            if (id != song.songId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.songId))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", song.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", song.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", song.GenreId);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .Include(s => s.Genres)
                .FirstOrDefaultAsync(m => m.songId == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Song'  is null.");
            }
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _context.Song.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
          return (_context.Song?.Any(e => e.songId == id)).GetValueOrDefault();
        }
    }
}
