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
    public class PlaylistDataAccess : IPlaylistDataAccess
    {
        private const string _constr = @"Data Source=SIT-SROLAP1099; Initial Catalog=Muzica; integrated security=true";
        private readonly SqlConnection _connection;

        public PlaylistDataAccess()
        {
            _connection = new SqlConnection(_constr);
        }

        public bool AddPlaylist(Playlist playlist)
        {
            var results = false;

            var sqlQuery = "INSERT INTO Playlist (NumePlaylist, NumarPiese, Gen) VALUES (@numePlaylist, @numarPiese, @gen)";


            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("numePlaylist", playlist.NumePlaylist));
                cmd.Parameters.Add(new SqlParameter("numarPiese", playlist.NumarPiese));
                cmd.Parameters.Add(new SqlParameter("gen", playlist.Gen));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public bool AddMelodieToPlaylist(int idPlaylist, int idMelodie)
        {
            var results = false;

            var sqlQuery = "INSERT INTO MelodiePlaylist (IdPlaylist, IdMelodie) VALUES (@idPlaylist, @idMelodie)";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("idPlaylist", idPlaylist));
                cmd.Parameters.Add(new SqlParameter("idMelodie", idMelodie));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public bool UpdatePlaylist(Playlist playlist)
        {
            var results = false;

            //sql Query
            GetPlaylist();
            var sqlQuery = "UPDATE Playlist " +
                            "SET " +
                            "NumePlaylist=@numePlaylist, NumarPiese=@numarPiese, Gen=@gen " +
                            "WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", playlist.Id));
                cmd.Parameters.Add(new SqlParameter("numePlaylist", playlist.NumePlaylist));
                cmd.Parameters.Add(new SqlParameter("numarPiese", playlist.NumarPiese));
                cmd.Parameters.Add(new SqlParameter("gen", playlist.Gen));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool UpdatePlaylistRiseNumarPiese(int playlistId)
        {
            var results = false;

            //sql Query
            GetPlaylist();
            var sqlQuery = "update Playlist "+
                            "set NumarPiese = NumarPiese + 1 "+
                            "WHERE Id=@playlistId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("playlistId", playlistId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool UpdatePlaylistDecreaseNumarPiese(int playlistId)
        {
            var results = false;

            //sql Query
            GetPlaylist();
            var sqlQuery = "update Playlist " +
                            "set NumarPiese = NumarPiese - 1 " +
                            "WHERE Id=@playlistId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("playlistId", playlistId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool UpdatePlaylistGenre(string gen, int playlistId)
        {
            var results = false;

            //sql Query
            GetPlaylist();
            var sqlQuery = "update Playlist " +
                            "set Gen = @gen " +
                            "WHERE Id=@playlistId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("gen", gen));
                cmd.Parameters.Add(new SqlParameter("playlistId", playlistId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public Playlist GetPlaylistById(int id)
        {
            Playlist playlist = new Playlist();
            var results = false;
            var sqlQuery = "SELECT Id, NumePlaylist, NumarPiese, Gen FROM Playlist WHERE Id=@id";
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
                        playlist = new Playlist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            NumePlaylist = sdr["NumePlaylist"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            Gen = sdr["Gen"].ToString()
                        };
                    }
                    else playlist = null;
                }

                _connection.Close();
            }
            return playlist;
        }

        public IEnumerable<Playlist> GetPlaylist()
        {
            List<Playlist> playlist = new List<Playlist>();

            var sqlQuery = "SELECT Id, NumePlaylist, NumarPiese, Gen FROM Playlist";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        playlist.Add(new Playlist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            NumePlaylist = sdr["NumePlaylist"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            Gen = sdr["Gen"].ToString()

                        });
                    }
                }
                _connection.Close();
            }


            return playlist;
        }

        public IEnumerable<Playlist> GetPlaylistBySongNotIn(int melodieId)
        {
            List<Playlist> playlist = new List<Playlist>();

            var sqlQuery = "SELECT Playlist.Id, Playlist.NumePlaylist, Playlist.NumarPiese, Playlist.Gen FROM Playlist " +
                            "WHERE Playlist.Id NOT IN " +
                            "( " +
                            "SELECT mpl.IdPlaylist FROM MelodiePlaylist mpl, Melodie m, Playlist pl " +
                            "WHERE pl.Id = mpl.IdPlaylist AND mpl.IdMelodie = @melodieId " +
                            ")";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("melodieId", melodieId));
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        playlist.Add(new Playlist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            NumePlaylist = sdr["NumePlaylist"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            Gen = sdr["Gen"].ToString()

                        });
                    }
                }
                _connection.Close();
            }


            return playlist;
        }

        public IEnumerable<Playlist> GetPlaylistBySongIn(int melodieId)
        {
            List<Playlist> playlist = new List<Playlist>();

            var sqlQuery = "SELECT Playlist.Id, Playlist.NumePlaylist, Playlist.NumarPiese, Playlist.Gen FROM Playlist " +
                            "WHERE Playlist.Id IN " +
                            "( " +
                            "SELECT mpl.IdPlaylist FROM MelodiePlaylist mpl, Melodie m, Playlist pl " +
                            "WHERE pl.Id = mpl.IdPlaylist AND mpl.IdMelodie = @melodieId " +
                            ")";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("melodieId", melodieId));
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        playlist.Add(new Playlist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            NumePlaylist = sdr["NumePlaylist"].ToString(),
                            NumarPiese = Convert.ToInt32(sdr["NumarPiese"]),
                            Gen = sdr["Gen"].ToString()
                        });
                    }
                }
                _connection.Close();
            }

            return playlist;
        }

        public bool GetMelodiePlaylistByBothIds(int idPlaylist, int idMelodie)   //era utila pentru a nu avea la add recomandare
        {                                                                                   //de playlist in care am adaugat deja melodie
            MelodiePlaylist melodiePlaylist = new MelodiePlaylist();                        //af fost rezolvata din dropdownlist
            var results = false;
            //var sqlQuery = "SELECT Id, IdPlaylist, IdMelodie FROM MelodiePlaylist WHERE IdPlaylist=@idPlaylist AND IdMelodie=@idMelodie";
            var sqlQuery = "SELECT Id FROM MelodiePlaylist WHERE IdPlaylist=@idPlaylist AND IdMelodie=@idMelodie";
            int melodiePlaylistId=0;
            //SqlCommand
            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("idPlaylist", idPlaylist));
                cmd.Parameters.Add(new SqlParameter("idMelodie", idMelodie));

                var affectedRows = cmd.ExecuteNonQuery();
                //results = affectedRows > 0;
                //SqlDataReader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        results = true;
                        melodiePlaylistId = Convert.ToInt32(sdr["Id"]);
                    }
                    else results = false;
                }

                _connection.Close();
            }
            return results;
        }

        public bool DeletePlaylist(Playlist playlist)
        {
            var results = false;

            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM Playlist WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("Id", playlist.Id));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool DeleteMelodieFromPlaylist(int playlistId, int melodieId)
        {
            var results = false;

            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM MelodiePlaylist WHERE IdPlaylist = @playlistId AND IdMelodie = @melodieId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("playlistId", playlistId));
                cmd.Parameters.Add(new SqlParameter("melodieId", melodieId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }
    }
}
