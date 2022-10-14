using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicMVC.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistBL playlistBL;

        private readonly IMelodieBL melodieBL;
        public PlaylistController(IPlaylistBL playlistBL, IMelodieBL melodieBL)
        {
            this.playlistBL = playlistBL;
            this.melodieBL = melodieBL;
        }

        public IActionResult Index()
        {
            IEnumerable<Playlist> playlist = playlistBL.GetPlaylist();
            
            return View(playlist);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            playlistBL.AddPlaylist(playlist);
            return RedirectToAction("Index"); 
        }


        public IActionResult Edit(int playlistId)
        {

            Playlist playlist = playlistBL.GetPlaylistById(playlistId);
            return View(playlist);
        }

        [HttpPost]
        public IActionResult Edit(Playlist playlist)
        {
            playlistBL.UpdatePlaylist(playlist);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int playlistId)
        {

            Playlist playlist = playlistBL.GetPlaylistById(playlistId);
            IEnumerable<Playlist> playlists = playlistBL.GetPlaylist();

            ViewBag.GenSelectList = new SelectList(new string[] { "Pop", "Jazz", "Rock", "Rap", "Empty" });

            ViewBag.PlaylistSelectList = new SelectList(playlists, "Id", "NumePlaylist");

            return View(playlist);
        }

        [HttpPost]
        public IActionResult Details(int playlistId, Playlist playlist)
        {
            string gen = Request.Form["Gen"].ToString();
            string copyPlaylist = Request.Form["CopyPlaylist"].ToString();


            if (!string.IsNullOrEmpty(gen))
            {
                Console.WriteLine("am apasat pe un Gen:");
                Console.WriteLine(gen);
                Console.WriteLine();
                playlistBL.UpdatePlaylistGenre(gen, playlistId);
            }

            if (!string.IsNullOrEmpty(copyPlaylist))
            {
                Console.WriteLine("am apasat pe un Playlist:");
                int idSecondPlaylist = Convert.ToInt32(copyPlaylist);
                Console.WriteLine(idSecondPlaylist);
                Console.WriteLine();
                playlistBL.AddPlaylistSongsToPlaylist(playlistId, idSecondPlaylist);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int playlistId)
        {
            Playlist playlist = playlistBL.GetPlaylistById(playlistId);
            playlistBL.DeletePlaylist(playlist);
            return RedirectToAction("Index");
        }


        // HELP HELP HELP HELP
        private static int SavePlaylistId; //cum scap de SavePlaylistId si sa trimit mai departe de la un
                                    //view la altul valoarea playlistId

        public void setSavePlaylistId(int id)
        {
            SavePlaylistId = id;
        }

        public int getSavePlaylistId()
        {
            return SavePlaylistId;
        }

        public IActionResult List(int playlistId)
        {
            setSavePlaylistId(playlistId);
            
            SavePlaylistId = playlistId;
            Console.WriteLine(SavePlaylistId);

            IEnumerable<Melodie> melodie = melodieBL.GetMelodiiFromPlaylist(playlistId);

            IEnumerable<Melodie> notAddedMelody = playlistBL.NotAddedMelody(melodie, playlistId);

            ViewBag.SongsSelectList = new SelectList(notAddedMelody, "Id", "Nume");

            return View(melodie);
        }

        [HttpPost]
        public IActionResult List(int playlistId, Playlist playlist)
        {
            string songId = Request.Form["SongId"].ToString();

            if (!string.IsNullOrEmpty(songId))
            {
                playlistBL.AddMelodieToPlaylist(playlistId, Convert.ToInt32(songId));
            }

            return RedirectToAction("List", new { playlistId });
        }

        public IActionResult Remove(int melodieId)
        {
            Console.WriteLine(getSavePlaylistId());
            Console.WriteLine(melodieId);
            Console.WriteLine();

            playlistBL.DeleteMelodieFromPlaylist(getSavePlaylistId(), melodieId);
            return RedirectToAction("List", new {playlistId=getSavePlaylistId()});
        }

        public IActionResult PlayMusic(int param1) //param1 acelasi nume ca in View
        {
            Console.WriteLine(getSavePlaylistId());

            IEnumerable<Melodie> listaMelodii = melodieBL.GetMelodiiFromPlaylist(getSavePlaylistId());

            if(param1 > listaMelodii.Count()-1) param1 = 0;

            if (param1 < 0) param1 = listaMelodii.Count() - 1;
            

            Melodie playingSong = listaMelodii.ElementAt(param1);

            ViewBag.PlayingSongView = playingSong.Nume;

            return View(param1);
        }
    }
}
