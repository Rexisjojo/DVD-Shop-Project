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
    public class NewsContentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsContentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsContents
        public async Task<IActionResult> Index()
        {
              return _context.NewsContents != null ? 
                          View(await _context.NewsContents.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NewsContents'  is null.");
        }

        // GET: NewsContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewsContents == null)
            {
                return NotFound();
            }

            var newsContent = await _context.NewsContents
                .FirstOrDefaultAsync(m => m.NewsContentId == id);
            if (newsContent == null)
            {
                return NotFound();
            }

            return View(newsContent);
        }

        // GET: NewsContents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsContentId,NewsTitle,News")] NewsContent newsContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsContent);
        }

        // GET: NewsContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewsContents == null)
            {
                return NotFound();
            }

            var newsContent = await _context.NewsContents.FindAsync(id);
            if (newsContent == null)
            {
                return NotFound();
            }
            return View(newsContent);
        }

        // POST: NewsContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsContentId,NewsTitle,News")] NewsContent newsContent)
        {
            if (id != newsContent.NewsContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsContentExists(newsContent.NewsContentId))
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
            return View(newsContent);
        }

        // GET: NewsContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewsContents == null)
            {
                return NotFound();
            }

            var newsContent = await _context.NewsContents
                .FirstOrDefaultAsync(m => m.NewsContentId == id);
            if (newsContent == null)
            {
                return NotFound();
            }

            return View(newsContent);
        }

        // POST: NewsContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewsContents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NewsContents'  is null.");
            }
            var newsContent = await _context.NewsContents.FindAsync(id);
            if (newsContent != null)
            {
                _context.NewsContents.Remove(newsContent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsContentExists(int id)
        {
          return (_context.NewsContents?.Any(e => e.NewsContentId == id)).GetValueOrDefault();
        }
    }
}
