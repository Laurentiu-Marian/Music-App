using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IPlaylistDataAccess
    {
        public bool AddPlaylist(Playlist playlist);
        public bool AddMelodieToPlaylist(int idPlaylist, int idMelodie);
        public IEnumerable<Playlist> GetPlaylist();
        public Playlist GetPlaylistById(int id);
        public IEnumerable<Playlist> GetPlaylistBySongNotIn(int id);
        public IEnumerable<Playlist> GetPlaylistBySongIn(int id);
        public bool GetMelodiePlaylistByBothIds(int idPlaylist, int idMelodie);
        public bool UpdatePlaylist(Playlist playlist);
        public bool UpdatePlaylistRiseNumarPiese(int Id);
        public bool UpdatePlaylistDecreaseNumarPiese(int playlistId);
        public bool UpdatePlaylistGenre(string gen, int playlistId);
        public bool DeletePlaylist(Playlist playlist);
        public bool DeleteMelodieFromPlaylist(int playlistId, int melodieId);
    }
}
