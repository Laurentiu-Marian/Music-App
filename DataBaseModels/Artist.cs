using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class Artist
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public int Varsta { get; set; }
        public string Adresa { get; set; }
        public string? Telefon { get; set; }
        public string? NumeDeScena { get; set; }
    }
}
