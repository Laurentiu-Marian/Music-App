using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IArtistDataAccess
    {
        bool AddArtist(Artist artist);
        public bool AddMelodieToArtist(int idArtist, int idMelodie);
        IEnumerable<Artist> GetArtists();
        public IEnumerable<Artist> GetArtistBySongNotIn(int id);
        public bool GetArtistMelodieByBothIds(int idArtist, int idMelodie);
        bool UpdateArtist(Artist artist);
        Artist GetArtistsById(int id);
        bool DeleteArtist(Artist artist);
    }
}
