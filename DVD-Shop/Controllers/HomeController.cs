using DVD_Shop.Data;
using DVD_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DVD_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public  IActionResult Index() 
        { 
            return View( _context.Games.ToList());
              

        }
        public  IActionResult AlbumAll() 
        { 
            return View( _context.Albums.ToList());
              

        }

        public IActionResult ArtistsAll()
        {
            return View(_context.Artists.ToList());


        }

        public IActionResult MoviesAll()
        {
            return View(_context.Movies.ToList());


        }

        public IActionResult SongsAll()
        {
            return View(_context.Song.ToList());


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}