using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Web.Mvc.Controls;

namespace MusicMVC.Controllers
{
    public class MelodieController : Controller
    {
        private readonly IMelodieBL melodieBL;

        private readonly IPlaylistBL playlistBL;

        private readonly IArtistBL artistBL;

        private readonly IAlbumBL albumBL;

        private static int saveAlbumId;

        private static string saveAlert = null;

        public MelodieController(IMelodieBL melodieBL, IPlaylistBL playlistBL, IArtistBL artistBL, IAlbumBL albumBL)
        {
            this.melodieBL = melodieBL;
            this.playlistBL = playlistBL;
            this.artistBL = artistBL;
            this.albumBL = albumBL;
        }

        public IActionResult Index()
        {
            IEnumerable<Melodie> melodie = melodieBL.GetMelodii();
            ViewBag.SuccessMessage = GetSaveAlert();
            SetSaveAlert(null);
            return View(melodie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Melodie melodie)
        {
            melodieBL.AddMelodie(melodie);
            return RedirectToAction("index");
        }

        public void SetSaveAlert(string message)
        {
            saveAlert = message;
        }

        public string GetSaveAlert()
        {
            return saveAlert;
        }

        public void setSaveAlbumId(int id)
        {
            saveAlbumId = id;
        }

        public int getSaveAlbumId()
        {
            return saveAlbumId;
        }

        public IActionResult Edit(int melodieId)
        {
            Melodie melodie = melodieBL.GetMelodiiById(melodieId);
            setSaveAlbumId(melodie.IdAlbum);
            return View(melodie);
        }

        [HttpPost]
        public IActionResult Edit(Melodie melodie)
        {
            melodieBL.UpdateMelodie(melodie);
            albumBL.UpdateNumberOfSongsInAlbum(getSaveAlbumId(), melodie.IdAlbum);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int melodieId, Playlist playlists)
        {
            Melodie melodie = melodieBL.GetMelodiiById(melodieId);
            
            IEnumerable<Playlist> playlist = playlistBL.GetPlaylistBySongNotIn(melodieId);
            IEnumerable<Artist> artist = artistBL.GetArtistBySongNotIn(melodieId);


            ViewBag.PlaylistSelectList = new SelectList(playlist, "Id", "NumePlaylist");
            ViewBag.ArtistSelectList = new SelectList(artist, "Id", "Nume");

            return View(melodie);
        }

        [HttpPost]
        public IActionResult Details(int melodieId)
        {
            string IdPlaylist = Request.Form["IdP"].ToString();
            string IdArtist = Request.Form["IdA"].ToString();

            
            ViewBag.SuccessMessage = "Alerta";

            if (!string.IsNullOrEmpty(IdPlaylist))
            {
                int playlistId = Convert.ToInt32(IdPlaylist);
                
                Console.WriteLine("am apasat pe un playlist:");
                Console.WriteLine(IdPlaylist);
                Console.WriteLine();

                /////// Alert Message
                
                var confirm = playlistBL.AddMelodieToPlaylist(playlistId, melodieId);

                if (confirm == false)
                {
                    Melodie melodie = melodieBL.GetMelodiiById(melodieId);
                    SetSaveAlert("Melodia nu a putut fi adaugata");

                }
                else
                {
                    SetSaveAlert("Melodie adaugata cu succes");
                }
                ///////
            }

            if (!string.IsNullOrEmpty(IdArtist))
            {
                int artistId = Convert.ToInt32(IdArtist);

                Console.WriteLine("am apasat pe un artist:");
                Console.WriteLine(IdArtist);
                Console.WriteLine();
                artistBL.AddMelodieToArtist(artistId, melodieId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int melodieId)
        {
            Melodie melodie = melodieBL.GetMelodiiById(melodieId);

            IEnumerable<Playlist> playlist = playlistBL.GetPlaylistBySongIn(melodieId);

            melodieBL.DeleteMelodie(melodie);

            List<int> idlist = playlist.Select(c => c.Id).ToList();

            Console.WriteLine(idlist.Count);

            for (int i = 0; i < idlist.Count; i++)
            {
                Console.WriteLine(idlist[i]);
                playlistBL.UpdatePlaylistDecreaseNumarPiese(idlist[i]);
            }

            return RedirectToAction("Index");
        }

        public IActionResult MelodieRiseAprecieri(int melodieId)
        {
            Console.WriteLine("Ai dat like la melodia");
            Console.WriteLine(melodieId);
            melodieBL.UpdateMelodieRiseAprecieri(melodieId);
            return RedirectToAction("Index");
        }

        public IActionResult MelodieDecreaseAprecieri(int melodieId)
        {
            Console.WriteLine("Ai dat dislike la melodia");
            Console.WriteLine(melodieId);
            melodieBL.UpdateMelodieDecreaseAprecieri(melodieId);
            return RedirectToAction("Index");
        }


    }
}
