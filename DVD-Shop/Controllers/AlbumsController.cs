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
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;
        public AlbumsController(ApplicationDbContext context , IWebHostEnvironment iwc)
        {
            _context = context;
            iw = iwc;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Albums.Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Album album, IFormFile img)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(album);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
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
                    album.ImageURL = @"\Images\" + fname;
                    _context.Add(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
                //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", album.CategoryId);
                return View(album);

        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", album.CategoryId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Album album , IFormFile img)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }


            //try
            //{
            //    _context.Update(album);
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AlbumExists(album.AlbumId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
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
                    album.ImageURL = @"\Images\" + fname;
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
            //return RedirectToAction(nameof(Index));
            
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", album.CategoryId);
            return View(album);    //album was written in the brackets
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return (_context.Albums?.Any(e => e.AlbumId == id)).GetValueOrDefault();
        }
    }
}
