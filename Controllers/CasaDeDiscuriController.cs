using BLL.Abstract;
using DataBaseModels;
using Microsoft.AspNetCore.Mvc;

namespace MusicMVC.Controllers
{
    public class CasaDeDiscuriController : Controller
    {
        private readonly ICasaDeDiscuriBL casaDeDiscuriBL;
        public CasaDeDiscuriController(ICasaDeDiscuriBL casaDeDiscuriBL)
        {
            this.casaDeDiscuriBL = casaDeDiscuriBL;
        }

        public IActionResult Index()
        {
            IEnumerable<CasaDeDiscuri> casaDeDiscuri = casaDeDiscuriBL.GetCasaDeDiscuri();

            return View(casaDeDiscuri);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CasaDeDiscuri casaDeDiscuri)
        {
            casaDeDiscuriBL.AddCasaDeDiscuri(casaDeDiscuri);
            //var artisti = artistBL.GetArtists();  //1
            //return artisti;                       //1
            return RedirectToAction("index");       //2
        }


        public IActionResult Edit(int casaDeDiscuriId)
        {

            CasaDeDiscuri casaDeDiscuri = casaDeDiscuriBL.GetCasaDeDiscuriById(casaDeDiscuriId);
            return View(casaDeDiscuri);
        }

        [HttpPost]
        public IActionResult Edit(CasaDeDiscuri casaDeDiscuri)
        {
            casaDeDiscuriBL.UpdateCasaDeDiscuri(casaDeDiscuri);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int casaDeDiscuriId)
        {

            CasaDeDiscuri casaDeDiscuri = casaDeDiscuriBL.GetCasaDeDiscuriById(casaDeDiscuriId);
            return View(casaDeDiscuri);
        }

        public IActionResult Delete(int casaDeDiscuriId)
        {
            CasaDeDiscuri casaDeDiscuri = casaDeDiscuriBL.GetCasaDeDiscuriById(casaDeDiscuriId);
            casaDeDiscuriBL.DeleteCasaDeDiscuri(casaDeDiscuri);
            return RedirectToAction("Index");
        }
    }
}
