using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using Music.Models;
using System.Diagnostics;

namespace Music.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMelodieBL melodieBL;

        private readonly IPlaylistBL playlistBL;

        private readonly IArtistBL artistBL;


        public HomeController(ILogger<HomeController> logger, IMelodieBL melodieBL, IPlaylistBL playlistBL, IArtistBL artistBL)
        {
            _logger = logger;

            this.melodieBL = melodieBL;
            this.playlistBL = playlistBL;
            this.artistBL = artistBL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Am ajuns in pagina index din controllerul home");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ShowTopSongs()
        {
            IEnumerable<Melodie> melodie = melodieBL.ShowTopSongs() ;
            return View(melodie);
        }

        public IActionResult ShowTopArtists()
        {
            ViewBag.artist = melodieBL.ShowTopArtists();
            return View();
        }

        public IActionResult ShowTopGenres()
        {
            ViewBag.gen = melodieBL.ShowTopGenres();
            return View();
        }
    }
}