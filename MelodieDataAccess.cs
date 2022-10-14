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
    public class MelodieDataAccess : IMelodieDataAccess
    {
        private const string _constr = @"Data Source=SIT-SROLAP1099; Initial Catalog=Muzica; integrated security=true";
        private readonly SqlConnection _connection;

        public MelodieDataAccess()
        {
            _connection = new SqlConnection(_constr);
        }

        public bool AddMelodie(Melodie melodie)
        {
            var results = false;

            var sqlQuery = "INSERT INTO Melodie (Nume, Gen, Durata, DataLansare, IdAlbum, Aprecieri) VALUES (@nume, @gen, @durata, @dataLansare, @idAlbum, @aprecieri)";


            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("nume", melodie.Nume));
                cmd.Parameters.Add(new SqlParameter("gen", melodie.Gen));
                cmd.Parameters.Add(new SqlParameter("durata", melodie.Durata));
                cmd.Parameters.Add(new SqlParameter("dataLansare", melodie.DataLansare));
                cmd.Parameters.Add(new SqlParameter("idAlbum", melodie.IdAlbum));
                cmd.Parameters.Add(new SqlParameter("aprecieri", melodie.Aprecieri));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public IEnumerable<Melodie> GetMelodii()
        {
            List<Melodie> melodii = new List<Melodie>();

            var sqlQuery = "SELECT Id, Nume, Gen, Durata, DataLansare, IdAlbum, Aprecieri FROM Melodie";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        melodii.Add(new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        });
                    }
                }
                _connection.Close();
            }
            return melodii;
        }

        public bool UpdateMelodie(Melodie melodie)
        {
            var results = false;

            //sql Query
            GetMelodii();
            var sqlQuery = "UPDATE Melodie " +
                            "SET " +
                            "Nume=@nume, Gen=@gen, Durata=@durata, DataLansare=@dataLansare, IdAlbum=@idAlbum, Aprecieri=@aprecieri " +
                            "WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", melodie.Id));
                cmd.Parameters.Add(new SqlParameter("nume", melodie.Nume));
                cmd.Parameters.Add(new SqlParameter("gen", melodie.Gen));
                cmd.Parameters.Add(new SqlParameter("durata", melodie.Durata));
                cmd.Parameters.Add(new SqlParameter("dataLansare", melodie.DataLansare));
                cmd.Parameters.Add(new SqlParameter("idAlbum", melodie.IdAlbum));
                cmd.Parameters.Add(new SqlParameter("aprecieri", melodie.Aprecieri));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public Melodie GetMelodiiById(int id)
        {
            Melodie melodie = new Melodie();
            var results = false;
            var sqlQuery = "SELECT Id, Nume, Gen, Durata, DataLansare, IdAlbum, Aprecieri FROM Melodie WHERE Id=@id";
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
                        melodie = new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        };
                    }
                    else melodie = null;
                }

                _connection.Close();
            }
            return melodie;
        }

        public bool DeleteMelodie(Melodie melodie)
        {
            var results = false;

            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM Melodie WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("Id", melodie.Id));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public IEnumerable<Melodie> GetMelodiiFromPlaylist(int playlistId)
        {
            List<Melodie> melodie = new List<Melodie>();

            var sqlQuery = "SELECT * FROM Melodie " +
                            "WHERE Melodie.Id IN " +
                            "( " +
                            "SELECT mpl.IdMelodie FROM MelodiePlaylist mpl, Melodie m, Playlist pl " +
                            "WHERE m.Id=mpl.IdMelodie AND mpl.IdPlaylist = @playlistId " +
                            ")";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("playlistId", playlistId));
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        melodie.Add(new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        });
                    }
                }
                _connection.Close();
            }


            return melodie;
        }

        public IEnumerable<Melodie> GetMelodiiFromArtist(int artistId)
        {
            List<Melodie> melodie = new List<Melodie>();

            var sqlQuery = "SELECT * FROM Melodie " +
                            "WHERE Melodie.Id IN " +
                            "( " +
                            "SELECT am.IdMelodie FROM ArtistMelodie am, Melodie m, Artist a " +
                            "WHERE m.Id=am.IdMelodie AND am.IdArtist = @artistId " +
                            ")";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("artistId", artistId));
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        melodie.Add(new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        });
                    }
                }
                _connection.Close();
            }


            return melodie;
        }

        public IEnumerable<Melodie> GetMelodiiFromAlbum(int idAlbum)
        {
            List<Melodie> melodie = new List<Melodie>();

            var sqlQuery = "SELECT Id, Nume, Gen, Durata, DataLansare, IdAlbum, Aprecieri FROM Melodie WHERE IdAlbum=@idAlbum";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("idAlbum", idAlbum));
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        melodie.Add(new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        });
                    }
                }
                _connection.Close();
            }


            return melodie;
        }

        public bool UpdateMelodieRiseAprecieri(int melodieId)
        {
            var results = false;

            //sql Query
           
            var sqlQuery = "update Melodie " +
                            "set Aprecieri=Aprecieri+1 " +
                            "WHERE Id=@melodieId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("melodieId", melodieId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public bool UpdateMelodieDecreaseAprecieri(int melodieId)
        {
            var results = false;

            //sql Query

            var sqlQuery = "update Melodie " +
                            "set Aprecieri = Aprecieri - 1 " +
                            "WHERE Id=@melodieId";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("melodieId", melodieId));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public IEnumerable<Melodie> ShowTopSongs()
        {
            List<Melodie> melodii = new List<Melodie>();

            var sqlQuery = "select top(3) * from Melodie "+
                            "order by Aprecieri DESC";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        melodii.Add(new Melodie
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Gen = sdr["Gen"].ToString(),
                            Durata = sdr["Durata"].ToString(),
                            DataLansare = DateTime.Parse(sdr["DataLansare"].ToString()),
                            IdAlbum = Convert.ToInt32(sdr["IdAlbum"]),
                            Aprecieri = Convert.ToInt32(sdr["Aprecieri"])
                        });
                    }
                }
                _connection.Close();
            }
            return melodii;
        }

        public List<string> ShowTopArtists()
        {
            List<string> artisti = new List<string>();

            var sqlQuery = "SELECT TOP(3) T.Nume from( "+
                            "SELECT a.Id, a.Nume, m.Aprecieri FROM ArtistMelodie am, Melodie m, Artist a "+
                            "WHERE am.IdArtist = a.Id AND am.IdMelodie = m.Id "+
                            ") as T "+
                            "group by T.Nume "+
                            "order by sum(T.Aprecieri) DESC";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;            

                _connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        artisti.Add(sdr["Nume"].ToString());
                    }
                }
                _connection.Close();
            }
            return artisti;
        }

        public List<string> ShowTopGenres()
        {
            List<string> melodii = new List<string>();

            var sqlQuery = "select top(3) Gen from Melodie "+
                            "group by Gen "+
                            "order by sum(Aprecieri) DESC";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;

                _connection.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        melodii.Add(sdr["Gen"].ToString());
                    }
                }
                _connection.Close();
            }
            return melodii;
        }
    }
}
