using BLL.Abstract;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CasaDeDiscuriBL : ICasaDeDiscuriBL
    {
        private readonly ICasaDeDiscuriDataAccess _casaDeDiscuriDataAccess;
        public CasaDeDiscuriBL(ICasaDeDiscuriDataAccess casaDeDiscuriDataAccess)
        {
            _casaDeDiscuriDataAccess = casaDeDiscuriDataAccess;
        }

        public IEnumerable<CasaDeDiscuri> GetCasaDeDiscuri()
        {
            return _casaDeDiscuriDataAccess.GetCasaDeDiscuri();
        }

        public bool AddCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            return _casaDeDiscuriDataAccess.AddCasaDeDiscuri(casaDeDiscuri);
        }

        public bool UpdateCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            var casaDeDiscuriFromDb = _casaDeDiscuriDataAccess.GetCasaDeDiscuriById(casaDeDiscuri.Id);
            if (casaDeDiscuriFromDb == null) return false;
            return _casaDeDiscuriDataAccess.UpdateCasaDeDiscuri(casaDeDiscuri);
        }

        public CasaDeDiscuri GetCasaDeDiscuriById(int id)
        {
            return _casaDeDiscuriDataAccess.GetCasaDeDiscuriById(id);
        }

        public bool DeleteCasaDeDiscuri(CasaDeDiscuri casaDeDiscuri)
        {
            var casaDeDiscuriFromDb = _casaDeDiscuriDataAccess.GetCasaDeDiscuriById(casaDeDiscuri.Id);
            if (casaDeDiscuriFromDb == null) return false;
            return _casaDeDiscuriDataAccess.DeleteCasaDeDiscuri(casaDeDiscuri);
        }
    }
}
