using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVD_Shop.Data;
using DVD_Shop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DVD_Shop.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;

        public MoviesController(ApplicationDbContext context, IWebHostEnvironment iwc)
        {
            _context = context;
            iw = iwc;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Album).Include(m => m.Artist).Include(m => m.Genres);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Album)
                .Include(m => m.Artist)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "Title");
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "aName");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "gName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Movie movie, IFormFile img, IFormFile video)
        {
            try
            {

                if (img != null)
                {
                    string ext = Path.GetExtension(img.FileName);
                    if (ext == ".jpg" || ext == ".gif" || ext == ".png")
                    {
                        string d = Path.Combine(iw.WebRootPath, "Images");
                        var fname = Path.GetFileName(img.FileName);
                        string filePath = Path.Combine(d, fname);
                        using (var fs = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(fs);
                        }
                        movie.CoverImage = @"\Images\" + fname;
                        _context.Add(movie);
                        //await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.m = "Wrong Picture Format";
                    }
                }


                if (video != null)
                {
                    string ext = Path.GetExtension(video.FileName);
                    if (ext == ".mp4" || ext == ".avi" || ext == ".mov")
                    {
                        string directory = Path.Combine(iw.WebRootPath, "Videos");
                        var fileName = Path.GetFileName(video.FileName);
                        string filePath = Path.Combine(directory, fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await video.CopyToAsync(fileStream);
                        }
                        movie.TrailerPath = @"\Videos\" + fileName;
                        _context.Add(movie);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = "Wrong Video Format";
                    }
                }

            }
            finally
            { ViewBag.Message = "ERROR"; }
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", album.CategoryId);
         
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", movie.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", movie.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", movie.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", movie.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            ViewData["AlbumId"] = new SelectList(_context.Albums, "AlbumId", "AlbumId", movie.AlbumId);
            ViewData["ArtistId"] = new SelectList(_context.Artists, "ArtistId", "ArtistId", movie.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Album)
                .Include(m => m.Artist)
                .Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
