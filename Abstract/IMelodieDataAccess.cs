using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IMelodieDataAccess
    {
        IEnumerable<Melodie> GetMelodii();

        bool AddMelodie(Melodie melodie);

        bool UpdateMelodie(Melodie melodie);

        Melodie GetMelodiiById(int id);

        public IEnumerable<Melodie> GetMelodiiFromPlaylist(int playlistId);

        public IEnumerable<Melodie> GetMelodiiFromArtist(int artistId);

        public IEnumerable<Melodie> GetMelodiiFromAlbum(int idAlbum);

        bool DeleteMelodie(Melodie melodie);

        public bool UpdateMelodieRiseAprecieri(int melodieId);

        public bool UpdateMelodieDecreaseAprecieri(int melodieId);

        public IEnumerable<Melodie> ShowTopSongs();

        public List<string> ShowTopArtists();

        public List<string> ShowTopGenres();
    }
}
