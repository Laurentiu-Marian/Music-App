using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class MelodiePlaylist
    {
        public int Id { get; set; }
        public int IdPlaylist { get; set; }
        public int IdMelodie { get; set; }
    }
}
