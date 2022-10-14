using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AlbumDataAccess : IAlbumDataAccess
    {
        private const string _constr = @"Data Source=SIT-SROLAP1099; Initial Catalog=Muzica; integrated security=true";
        private readonly SqlConnection _connection;

        public AlbumDataAccess()
        {
            _connection = new SqlConnection(_constr);
        }

        public bool AddAlbum(Album album)
        {
            var results = false;

            var sqlQuery = "INSERT INTO Album (Nume, NumarPiese, DataLansare, GenAlbum, IdArtist) VALUES (@nume, @numarPiese, @dataLansare, @genAlbum, @idArtist)";


            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("nume", album.Nume));
                cmd.Parameters.Add(new SqlParameter("numarPiese", album.NumarPiese));
                cmd.Parameters.Add(new SqlParameter("dataLansare", album.DataLansare));
                cmd.Parameters.Add(new SqlParameter("genAlbum", album.GenAlbum));
                cmd.Parameters.Add(new SqlParameter("idArtist", album.IdArtist));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public IEnumerable<Album> GetAlbums()
        {
            List<Album> albums = new List<Album>();

            var sqlQuery = "SELECT Id, Nume, NumarPiese, DataLansare, GenAlbum, IdArtist FROM Album";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        albums.Add(new Album
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            GenAlbum = sdr["GenAlbum"].ToString(),
                            IdArtist = Convert.ToInt32(sdr["IdArtist"])
                        });
                    }
                }
                _connection.Close();
            }


            return albums;
        }

        public bool UpdateAlbum(Album album)
        {
            var results = false;

            //sql Query
            GetAlbums();
            var sqlQuery = "UPDATE Album " +
                            "SET " +
                            "Nume=@nume, NumarPiese=@numarPiese, DataLansare=@dataLansare, GenAlbum=@genAlbum, " +
                            "IdArtist=@idArtist WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", album.Id));
                cmd.Parameters.Add(new SqlParameter("nume", album.Nume));
                cmd.Parameters.Add(new SqlParameter("numarPiese", album.NumarPiese));
                cmd.Parameters.Add(new SqlParameter("dataLansare", album.DataLansare));
                cmd.Parameters.Add(new SqlParameter("genAlbum", album.GenAlbum));
                cmd.Parameters.Add(new SqlParameter("idArtist", album.IdArtist));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool RiseNumberOfSongsInAlbum(int albumId)
        {
            var results = false;

            //sql Query

            var sqlQuery = "update Album " +
                            "set NumarPiese=NumarPiese+1 " +
                            "WHERE Id=@albumId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("albumId", albumId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool DecreaseNumberOfSongsInAlbum(int albumId)
        {
            var results = false;

            //sql Query

            var sqlQuery = "update Album " +
                            "set NumarPiese=NumarPiese-1 " +
                            "WHERE Id=@albumId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("albumId", albumId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public Album GetAlbumsById(int id)
        {
            Album albums = new Album();
            var results = false;
            var sqlQuery = "SELECT Id, Nume, NumarPiese, DataLansare, GenAlbum, IdArtist FROM Album WHERE Id=@id";
            //SqlCommand
            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("Id", id));

                var affectedRows = cmd.ExecuteNonQuery();
                //results = affectedRows > 0;
                //SqlDataReader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        albums = new Album
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            GenAlbum = sdr["GenAlbum"].ToString(),
                            IdArtist = Convert.ToInt32(sdr["IdArtist"])
                        };
                    }
                    else albums = null;
                }

                _connection.Close();
            }
            return albums;
        }

        public bool DeleteAlbum(Album album)
        {
            var results = false;
            
            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM Album WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("Id", album.Id));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }
    }
}
