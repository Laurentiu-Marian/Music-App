using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IPlaylistBL
    {
        bool AddPlaylist(Playlist playlist);
        public bool AddMelodieToPlaylist(int idPlaylist, int idMelodie);
        public bool AddAllArtistsSongs(int playlistId, int artistId); //FARA DAL
        public bool AddAllAlbumsSongs(int playlistId, int albumId); //FARA DAL
        public bool AddPlaylistSongsToPlaylist(int idFirstPlaylist, int idSecondPlaylist); //FARA DAL
        public List<Melodie> NotAddedMelody(IEnumerable<Melodie> addedMelodiy, int playlistId);
        IEnumerable<Playlist> GetPlaylist();
        Playlist GetPlaylistById(int id);
        public IEnumerable<Playlist> GetPlaylistBySongNotIn(int id);
        public IEnumerable<Playlist> GetPlaylistBySongIn(int id);
        public bool GetMelodiePlaylistByBothIds(int idPlaylist, int idMelodie);
        bool UpdatePlaylist(Playlist playlist);
        public bool UpdatePlaylistRiseNumarPiese(int Id);
        public bool UpdatePlaylistDecreaseNumarPiese(int playlistId);
        public bool UpdatePlaylistGenre(string gen, int playlistId);
        bool DeletePlaylist(Playlist playlist);
        public bool DeleteMelodieFromPlaylist(int playlistId, int melodieId);

    }
}
