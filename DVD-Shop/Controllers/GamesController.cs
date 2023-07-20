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
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment iw;
        public GamesController(ApplicationDbContext context, IWebHostEnvironment iwc)
        {
            _context = context;
            iw = iwc;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return _context.Games != null ?
                         View(await _context.Games.ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Games'  is null.");
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.gameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Game game, IFormFile img , IFormFile video)
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
                        game.CoverImage = @"\Images\" + fname;
                        _context.Add(game);
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
                        game.TrailerPath = @"\Videos\" + fileName;
                        _context.Add(game);
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
                return View(game);
            
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Game game, IFormFile img, IFormFile video)
        {
            //if (id != game.gameId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(game);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!GameExists(game.gameId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

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
                        game.CoverImage = @"\Images\" + fname;
                        
                    }
                    else
                    {
                        ViewBag.m = "Wrong Picture Format";
                    }
                    _context.Update(game);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
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
                        game.TrailerPath = @"\Videos\" + fileName;
                        
                    }
                    else
                    {
                        ViewBag.Message = "Wrong Video Format";
                    }
                    _context.Update(game);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            finally
            { ViewBag.Message = "ERROR"; }

            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.gameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
          return (_context.Games?.Any(e => e.gameId == id)).GetValueOrDefault();
        }
    }
}
