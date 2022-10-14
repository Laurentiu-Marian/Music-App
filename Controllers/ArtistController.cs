using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicMVC.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistBL artistBL;
        private readonly IMelodieBL melodieBL;
        private readonly IPlaylistBL playlistBL;
        public ArtistController(IArtistBL artistBL, IMelodieBL melodieBL, IPlaylistBL playlistBL)
        {
            this.artistBL = artistBL;
            this.melodieBL = melodieBL;
            this.playlistBL = playlistBL;
        }

        public IActionResult Index()
        {
            IEnumerable<Artist> artisti = artistBL.GetArtists();

            return View(artisti);
        }


        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]        
        public IActionResult Create(Artist artist)
        {
            artistBL.AddArtist(artist);
            return RedirectToAction("index");
        }


        public IActionResult Edit(int artistId)
        {

            Artist artist = artistBL.GetArtistsById(artistId);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            artistBL.UpdateArtist(artist);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int artistId)
        {
            Artist artist = artistBL.GetArtistsById(artistId);

            IEnumerable<Playlist> playlists = playlistBL.GetPlaylist();
            ViewBag.PlaylistSelectList = new SelectList(playlists, "Id", "NumePlaylist");

            return View(artist);
        }

        [HttpPost]
        public IActionResult Details(int artistId, Playlist playlist)
        {
            string plst = Request.Form["Playlist"].ToString();

            if (!string.IsNullOrEmpty(plst))
            {
                int plstId = Convert.ToInt32(plst);
                Console.WriteLine("am apasat pe un Playlist:");
                Console.WriteLine(plstId);
                Console.WriteLine();
                playlistBL.AddAllArtistsSongs(plstId, artistId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult List(int artistId)
        {
            IEnumerable<Melodie> melodie = melodieBL.GetMelodiiFromArtist(artistId);
            return View(melodie);
        }

        public IActionResult Delete(int artistId)
        {
            Artist artist = artistBL.GetArtistsById(artistId);
            artistBL.DeleteArtist(artist);
            return RedirectToAction("Index");
        }


    }
}
