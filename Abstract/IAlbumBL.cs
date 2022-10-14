using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAlbumBL
    {
        bool AddAlbum(Album album);
        public bool UpdateNumberOfSongsInAlbum(int oldAlbumId, int newAlbumId);
        IEnumerable<Album> GetAlbums();
        Album GetAlbumsById(int id);
        bool UpdateAlbum(Album album);
        bool DeleteAlbum(Album album);
    }
}
