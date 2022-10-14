using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAlbumDataAccess
    {
        bool AddAlbum(Album album);
        IEnumerable<Album> GetAlbums();
        Album GetAlbumsById(int id);
        bool UpdateAlbum(Album album);
        public bool RiseNumberOfSongsInAlbum(int albumId);
        public bool DecreaseNumberOfSongsInAlbum(int albumId);
        bool DeleteAlbum(Album album);
    }
}
