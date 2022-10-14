using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class Album
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public int NumarPiese {  get; set; }
        public DateTime DataLansare { get; set; }
        public string GenAlbum { get; set; }
        public int IdArtist { get; set; }
    }
}
