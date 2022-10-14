using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IMelodieBL
    {
        IEnumerable<Melodie> GetMelodii();
        bool AddMelodie(Melodie melodie);
        Melodie GetMelodiiById(int id);
        public IEnumerable<Melodie> GetMelodiiFromAlbum(int idAlbum);
        public IEnumerable<Melodie> GetMelodiiFromPlaylist(int playlistId);
        public IEnumerable<Melodie> GetMelodiiFromArtist(int artistId);
        bool UpdateMelodie(Melodie melodie);
        bool DeleteMelodie(Melodie melodie);
        public bool UpdateMelodieRiseAprecieri(int melodieId);
        public bool UpdateMelodieDecreaseAprecieri(int melodieId);
        public IEnumerable<Melodie> ShowTopSongs();
        public List<string> ShowTopArtists();
        public List<string> ShowTopGenres();
    }
}
