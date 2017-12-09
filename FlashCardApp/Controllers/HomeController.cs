using FlashCardApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace FlashCardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlashCardAppDb _flashCardAppDb = new FlashCardAppDb(); 

        public ActionResult Index()
        {
            var model = _flashCardAppDb.Decks.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_flashCardAppDb != null)
            {
                _flashCardAppDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}