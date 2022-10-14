using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class Melodie
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Gen { get; set; }
        public string Durata { get; set; }
        public DateTime DataLansare {get; set; }
        public int IdAlbum { get; set; }
        public int Aprecieri { get; set; }
        //public int IdCasaDeDiscuri { get; set; }
    }
}
