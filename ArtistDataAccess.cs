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
    public class ArtistDataAccess : IArtistDataAccess
    {
        private const string _constr = @"Data Source=SIT-SROLAP1099; Initial Catalog=Muzica; integrated security=true";
        private readonly SqlConnection _connection;

        public ArtistDataAccess()
        {
            _connection = new SqlConnection(_constr);
        }

        public bool AddArtist( Artist artist)
        {
            var results = false;
            
            var sqlQuery = "INSERT INTO Artist (Nume, Varsta, Adresa, Telefon, NumeDeScena) VALUES (@nume, @varsta, @adresa, @telefon, @numeDeScena)";
            

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("nume", artist.Nume));
                cmd.Parameters.Add(new SqlParameter("varsta", artist.Varsta));
                cmd.Parameters.Add(new SqlParameter("adresa", artist.Adresa));
                cmd.Parameters.Add(new SqlParameter("telefon", artist.Telefon));
                cmd.Parameters.Add(new SqlParameter("numeDeScena", artist.NumeDeScena));
                
                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public bool AddMelodieToArtist(int idArtist, int idMelodie)
        {
            var results = false;

            var sqlQuery = "INSERT INTO ArtistMelodie (IdArtist, IdMelodie) VALUES (@idArtist, @idMelodie)";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("idArtist", idArtist));
                cmd.Parameters.Add(new SqlParameter("idMelodie", idMelodie));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public IEnumerable<Artist> GetArtists()
        {
            List<Artist> artists = new List<Artist>();
           
            var sqlQuery = "SELECT Id, Nume, Varsta, Adresa, Telefon, NumeDeScena FROM Artist";
            
            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        artists.Add(new Artist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Varsta = Convert.ToInt32(sdr["Varsta"]),
                            Adresa = sdr["Adresa"].ToString(),
                            Telefon = sdr["Telefon"].ToString(),
                            NumeDeScena = sdr["NumeDeScena"].ToString()
                        });
                    }
                }
                _connection.Close();
            }
            

            return artists;
        }

        public bool UpdateArtist(Artist artist)
        {
            var results = false;
            
            //sql Query
            GetArtists();
            var sqlQuery = "UPDATE Artist " +
                            "SET " +
                            "Nume=@nume, Varsta=@varsta, Adresa=@adresa, Telefon=@telefon, " +
                            "NumeDeScena=@numeDeScena WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", artist.Id));
                cmd.Parameters.Add(new SqlParameter("nume", artist.Nume));
                cmd.Parameters.Add(new SqlParameter("varsta", artist.Varsta));
                cmd.Parameters.Add(new SqlParameter("adresa", artist.Adresa));
                cmd.Parameters.Add(new SqlParameter("telefon", artist.Telefon));
                cmd.Parameters.Add(new SqlParameter("numeDeScena", artist.NumeDeScena));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }
            

            return results;
        }

        public Artist GetArtistsById(int id)
        {
            Artist artists = new Artist();
            var results = false;
            var sqlQuery = "SELECT Id, Nume, Varsta, Adresa, Telefon, NumeDeScena FROM Artist WHERE Id=@id";
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
                        artists = new Artist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Varsta = Convert.ToInt32(sdr["Varsta"]),
                            Adresa = sdr["Adresa"].ToString(),
                            Telefon = sdr["Telefon"].ToString(),
                            NumeDeScena = sdr["NumeDeScena"].ToString()
                        };
                    }
                    else artists = null;
                }

                _connection.Close();
            }
            return artists;
        }

        public bool GetArtistMelodieByBothIds(int idArtist, int idMelodie)   
        {                                                                                   
            ArtistMelodie artistMelodie = new ArtistMelodie();                     
            var results = false;
            var sqlQuery = "SELECT IdArtist, IdMelodie FROM ArtistMelodie WHERE IdArtist=@idArtist AND IdMelodie=@idMelodie";
            
            //SqlCommand
            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("idArtist", idArtist));
                cmd.Parameters.Add(new SqlParameter("idMelodie", idMelodie));

                var affectedRows = cmd.ExecuteNonQuery();
                //results = affectedRows > 0;
                //SqlDataReader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        results = true;
                    }
                    else results = false;
                }

                _connection.Close();
            }
            return results;
        }

        public IEnumerable<Artist> GetArtistBySongNotIn(int melodieId)
        {
            List<Artist> artist = new List<Artist>();

            var sqlQuery = "SELECT Artist.Id, Artist.Nume, Artist.Varsta, Artist.Adresa, Artist.Telefon, Artist.NumeDeScena FROM Artist " +
                            "WHERE Artist.Id NOT IN " +
                            "( " +
                            "SELECT am.IdArtist FROM ArtistMelodie am, Melodie m, Artist a " +
                            "WHERE a.Id = am.IdArtist AND am.IdMelodie = @melodieId " +
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
                        artist.Add(new Artist
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Nume = sdr["Nume"].ToString(),
                            Varsta = Convert.ToInt32(sdr["Varsta"]),
                            Adresa = sdr["Adresa"].ToString(),
                            Telefon = sdr["Telefon"].ToString(),
                            NumeDeScena = sdr["NumeDeScena"].ToString(),

                        });
                    }
                }
                _connection.Close();
            }
            return artist;
        }

        public bool DeleteArtist(Artist artist)
        {
            var results = false;

            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM Artist WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("Id", artist.Id));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }
    }
}
