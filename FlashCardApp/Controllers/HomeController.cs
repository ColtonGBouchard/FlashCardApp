using FlashCardApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashCardApp.Controllers
{
    public class HomeController : Controller
    {
        FlashCardAppDb _db = new FlashCardAppDb(); 

        public ActionResult Index()
        {
            var model = _db.Decks.ToList();
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
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}