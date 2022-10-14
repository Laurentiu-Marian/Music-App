using DataBaseModels;

namespace BLL.Abstract
{
    public interface IArtistBL
    {
        bool AddArtist(Artist artist);
        public bool AddMelodieToArtist(int idArtist, int idMelodie);
        IEnumerable<Artist> GetArtists();
        Artist GetArtistsById(int id);
        public IEnumerable<Artist> GetArtistBySongNotIn(int id);
        public bool GetArtistMelodieByBothIds(int idArtist, int idMelodie);
        bool UpdateArtist(Artist artist);
        bool DeleteArtist(Artist artist);
    }
}