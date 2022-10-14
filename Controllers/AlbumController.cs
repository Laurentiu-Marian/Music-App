using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicMVC.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumBL albumBL;
        private readonly IPlaylistBL playlistBL;
        private readonly IMelodieBL melodieBL;

        public AlbumController(IAlbumBL albumBL, IPlaylistBL playlistBL, IMelodieBL melodieBL)
        {
            this.albumBL = albumBL;
            this.playlistBL = playlistBL;
            this.melodieBL = melodieBL;
        }

        public IActionResult Index()
        {
            IEnumerable<Album> album = albumBL.GetAlbums();

            return View(album);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album)
        {
            albumBL.AddAlbum(album);
            return RedirectToAction("index");
        }


        public IActionResult Edit(int albumId)
        {

            Album album = albumBL.GetAlbumsById(albumId);
            return View(album);
        }

        [HttpPost]
        public IActionResult Edit(Album album)
        {
            albumBL.UpdateAlbum(album);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int albumId, Album albums)
        {

            Album album = albumBL.GetAlbumsById(albumId);

            IEnumerable<Playlist> playlists = playlistBL.GetPlaylist();
            ViewBag.PlaylistSelectList = new SelectList(playlists, "Id", "NumePlaylist");

            return View(album);
        }

        [HttpPost]
        public IActionResult Details(int albumId)
        {
            
            string IdPlaylist = Request.Form["Playlist"].ToString();

            if (!string.IsNullOrEmpty(IdPlaylist))
            {
                int playlistId = Convert.ToInt32(IdPlaylist);
                
                Console.WriteLine("am apasat pe un playlist:");
                Console.WriteLine(IdPlaylist);
                Console.WriteLine();
                playlistBL.AddAllAlbumsSongs(playlistId, albumId);

            }

            return RedirectToAction("Index");
        }

        public IActionResult List(int albumId)
        {
            IEnumerable<Melodie> melodie = melodieBL.GetMelodiiFromAlbum(albumId);
            return View(melodie);
        }

        public IActionResult Delete(int albumId)
        {
            Album album = albumBL.GetAlbumsById(albumId);
            albumBL.DeleteAlbum(album);
            return RedirectToAction("Index");
        }
    }
}
