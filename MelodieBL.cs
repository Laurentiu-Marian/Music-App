using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MelodieBL : IMelodieBL
    {

        private readonly IMelodieDataAccess _melodieDataAccess;
        public MelodieBL(IMelodieDataAccess melodieDataAccess)
        {
            _melodieDataAccess = melodieDataAccess;
        }

        public IEnumerable<Melodie> GetMelodii()
        {
            return _melodieDataAccess.GetMelodii();
        }

        public bool AddMelodie(Melodie melodie)
        {
            return _melodieDataAccess.AddMelodie(melodie);
        }

        public bool UpdateMelodie(Melodie melodie)
        {
            var melodieFromDb = _melodieDataAccess.GetMelodiiById(melodie.Id);
            if (melodieFromDb == null) return false;
            return _melodieDataAccess.UpdateMelodie(melodie);
        }

        public Melodie GetMelodiiById(int id)
        {
            return _melodieDataAccess.GetMelodiiById(id);
        }

        public IEnumerable<Melodie> GetMelodiiFromAlbum(int idAlbum)
        {
            return _melodieDataAccess.GetMelodiiFromAlbum(idAlbum);
        }

        public IEnumerable<Melodie> GetMelodiiFromPlaylist(int playlistId)
        {
            return _melodieDataAccess.GetMelodiiFromPlaylist(playlistId);
        }

        public IEnumerable<Melodie> GetMelodiiFromArtist(int artistId)
        {
            return _melodieDataAccess.GetMelodiiFromArtist(artistId);
        }

        public bool DeleteMelodie(Melodie melodie)
        {
            var melodieFromDb = _melodieDataAccess.GetMelodiiById(melodie.Id);
            if (melodieFromDb == null) return false;
            return _melodieDataAccess.DeleteMelodie(melodie);
        }

        public bool UpdateMelodieRiseAprecieri(int melodieId)
        {
            var melodieFromDb = _melodieDataAccess.GetMelodiiById(melodieId);
            if (melodieFromDb == null) return false;
            return _melodieDataAccess.UpdateMelodieRiseAprecieri(melodieId);
        }

        public bool UpdateMelodieDecreaseAprecieri(int melodieId)
        {
            var melodieFromDb = _melodieDataAccess.GetMelodiiById(melodieId);
            if (melodieFromDb == null) return false;
            return _melodieDataAccess.UpdateMelodieDecreaseAprecieri(melodieId);
        }

        public IEnumerable<Melodie> ShowTopSongs()
        {
            return _melodieDataAccess.ShowTopSongs();
        }

        public List<string> ShowTopArtists()
        {
            return _melodieDataAccess.ShowTopArtists();
        }

        public List<string> ShowTopGenres()
        {
            return _melodieDataAccess.ShowTopGenres();
        }
    }
}
