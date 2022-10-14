using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ArtistBL : IArtistBL
    {
        private readonly IArtistDataAccess _artistDataAccess;
        public ArtistBL(IArtistDataAccess artistDataAccess)
        {
            _artistDataAccess = artistDataAccess;
        }

        public IEnumerable<Artist> GetArtists()
        {
            return _artistDataAccess.GetArtists();
        }

        public IEnumerable<Artist> GetArtistBySongNotIn(int id)
        {
            return _artistDataAccess.GetArtistBySongNotIn(id);
        }

        public bool GetArtistMelodieByBothIds(int idArtist, int idMelodie)
        {
            return _artistDataAccess.GetArtistMelodieByBothIds(idArtist, idMelodie);
        }

        public bool AddArtist(Artist artist)
        {
            return _artistDataAccess.AddArtist(artist);
        }

        public bool AddMelodieToArtist(int idArtist, int idMelodie)
        {
            var melodiePlaylistFromDb = _artistDataAccess.GetArtistMelodieByBothIds(idArtist, idMelodie);
            if (melodiePlaylistFromDb == false)
            {
                return _artistDataAccess.AddMelodieToArtist(idArtist, idMelodie);
            }
            return false;
        }

        public bool UpdateArtist(Artist artist)
        {
            var artistFromDb = _artistDataAccess.GetArtistsById(artist.Id);
            if (artistFromDb == null) return false;
            return _artistDataAccess.UpdateArtist(artist);
        }

        public Artist GetArtistsById(int id)
        {
            return _artistDataAccess.GetArtistsById(id);
        }
        public bool DeleteArtist(Artist artist)
        {
            var artistFromDb = _artistDataAccess.GetArtistsById(artist.Id);
            if (artistFromDb == null) return false;
            return _artistDataAccess.DeleteArtist(artist);
        }
    }
}
