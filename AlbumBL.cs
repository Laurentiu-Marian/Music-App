using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AlbumBL : IAlbumBL
    {
        private readonly IAlbumDataAccess _albumDataAccess;
        public AlbumBL(IAlbumDataAccess albumDataAccess)
        {
            _albumDataAccess = albumDataAccess;
        }

        public IEnumerable<Album> GetAlbums()
        {
            return _albumDataAccess.GetAlbums();
        }

        public bool AddAlbum(Album album)
        {
            return _albumDataAccess.AddAlbum(album);
        }

        public bool UpdateAlbum(Album album)
        {
            var albumFromDb = _albumDataAccess.GetAlbumsById(album.Id);
            if (albumFromDb == null) return false;
            return _albumDataAccess.UpdateAlbum(album);
        }

        public bool UpdateNumberOfSongsInAlbum(int oldAlbumId, int newAlbumId)
        {
            Console.WriteLine(oldAlbumId);
            Console.WriteLine(newAlbumId);
            Console.WriteLine();
            if ((oldAlbumId == 0 && newAlbumId == 0) || (oldAlbumId == newAlbumId))
            {
                return false;
            }
            else if (oldAlbumId == 0 && newAlbumId != 0)
            {
                return _albumDataAccess.RiseNumberOfSongsInAlbum(newAlbumId);
            }
            else if (oldAlbumId != 0 && newAlbumId == 0)
            {
                return _albumDataAccess.DecreaseNumberOfSongsInAlbum(oldAlbumId);
            }
            else
            {
                _albumDataAccess.RiseNumberOfSongsInAlbum(newAlbumId);
                return _albumDataAccess.DecreaseNumberOfSongsInAlbum(oldAlbumId);
            }
            
            return true;
        }

        public Album GetAlbumsById(int id)
        {
            return _albumDataAccess.GetAlbumsById(id);
        }

        public bool DeleteAlbum(Album album)
        {
            var albumFromDb = _albumDataAccess.GetAlbumsById(album.Id);
            if (albumFromDb == null) return false;
            return _albumDataAccess.DeleteAlbum(album);
        }
    }
}
