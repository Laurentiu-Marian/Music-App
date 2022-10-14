using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICasaDeDiscuriBL
    {
        bool AddCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri);
        IEnumerable<CasaDeDiscuri> GetCasaDeDiscuri();
        CasaDeDiscuri GetCasaDeDiscuriById(int id);
        bool UpdateCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri);
        bool DeleteCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri);
    }
}
