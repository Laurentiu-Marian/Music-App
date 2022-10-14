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
    public class CasaDeDiscuriDataAccess : ICasaDeDiscuriDataAccess
    {
        private const string _constr = @"Data Source=SIT-SROLAP1099; Initial Catalog=Muzica; integrated security=true";
        private readonly SqlConnection _connection;

        public CasaDeDiscuriDataAccess()
        {
            _connection = new SqlConnection(_constr);
        }

        public bool AddCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            var results = false;

            var sqlQuery = "INSERT INTO CasaDeDiscuri (Adresa, Nume, CifraDeAfaceri, Email, Telefon) VALUES (@adresa, @nume, @cifraDeAfaceri, @email, @telefon)";


            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                cmd.Parameters.Add(new SqlParameter("adresa", casaDeDiscuri.Adresa));
                cmd.Parameters.Add(new SqlParameter("nume", casaDeDiscuri.Nume));
                cmd.Parameters.Add(new SqlParameter("cifraDeAfaceri", casaDeDiscuri.CifraDeAfaceri));
                cmd.Parameters.Add(new SqlParameter("email", casaDeDiscuri.Email));
                cmd.Parameters.Add(new SqlParameter("telefon", casaDeDiscuri.Telefon));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;

                _connection.Close();
            }

            return results;
        }

        public IEnumerable<CasaDeDiscuri> GetCasaDeDiscuri()
        {
            List<CasaDeDiscuri> casaDeDiscuri = new List<CasaDeDiscuri>();

            var sqlQuery = "SELECT Id, Adresa, Nume, CifraDeAfaceri, Email, Telefon FROM CasaDeDiscuri";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        casaDeDiscuri.Add(new CasaDeDiscuri
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Adresa = sdr["Adresa"].ToString(),
                            Nume = sdr["Nume"].ToString(),
                            CifraDeAfaceri = Convert.ToInt32(sdr["CifraDeAfaceri"]),
                            Email = sdr["Email"].ToString(),
                            Telefon = sdr["Telefon"].ToString()
                        });
                    }
                }
                _connection.Close();
            }


            return casaDeDiscuri;
        }

        public bool UpdateCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            var results = false;

            //sql Query
            GetCasaDeDiscuri();
            var sqlQuery = "UPDATE CasaDeDiscuri " +
                            "SET " +
                            "Adresa=@adresa, Nume=@nume, CifraDeAfaceri=@cifraDeAfaceri, Email=@email, Telefon=@telefon " +
                            "WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("id", casaDeDiscuri.Id));
                cmd.Parameters.Add(new SqlParameter("Adresa", casaDeDiscuri.Adresa));
                cmd.Parameters.Add(new SqlParameter("Nume", casaDeDiscuri.Nume));
                cmd.Parameters.Add(new SqlParameter("CifraDeAfaceri", casaDeDiscuri.CifraDeAfaceri));
                cmd.Parameters.Add(new SqlParameter("Email", casaDeDiscuri.Email));
                cmd.Parameters.Add(new SqlParameter("Telefon", casaDeDiscuri.Telefon));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }

        public CasaDeDiscuri GetCasaDeDiscuriById(int id)
        {
            CasaDeDiscuri casaDeDiscuri = new CasaDeDiscuri();
            var results = false;
            var sqlQuery = "SELECT Id, Adresa, Nume, CifraDeAfaceri, Email, Telefon FROM CasaDeDiscuri WHERE Id=@id";
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
                        casaDeDiscuri = new CasaDeDiscuri
                        {
                            Id = Convert.ToInt32(sdr["Id"]),
                            Adresa = sdr["Adresa"].ToString(),
                            Nume = sdr["Nume"].ToString(),
                            CifraDeAfaceri = Convert.ToInt32(sdr["CifraDeAfaceri"]),
                            Email = sdr["Email"].ToString(),
                            Telefon = sdr["Telefon"].ToString()
                        };
                    }
                    else casaDeDiscuri = null;
                }

                _connection.Close();
            }
            return casaDeDiscuri;
        }

        public bool DeleteCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            var results = false;

            //sql Query
            //GetAlbums();
            var sqlQuery = "DELETE FROM CasaDeDiscuri WHERE Id=@id";

            using (SqlCommand cmd = new SqlCommand(sqlQuery))
            {
                cmd.Connection = _connection;
                _connection.Open();
                cmd.Parameters.Add(new SqlParameter("Id", casaDeDiscuri.Id));

                var affectedRows = cmd.ExecuteNonQuery();
                results = affectedRows > 0;
                _connection.Close();
            }


            return results;
        }
    }
}
