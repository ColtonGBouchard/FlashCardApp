using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlashCardApp.Models;
using Microsoft.AspNet.Identity;

namespace FlashCardApp.Controllers
{
    public class DeckController : Controller
    {
        FlashCardAppDb db = new FlashCardAppDb();

        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();

            var model = (from d in db.Decks
                         where d.UserId == user
                         select d).ToList();

            return View(model);
        }
           
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,UserId")] Deck deck)
        {
            deck.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Decks.Add(deck);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deck);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,UserId")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        
        public ActionResult Delete(int? id)
        {
       
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = db.Decks.Find(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: /Deck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Card> cards = (from c in db.Cards
                                where c.DeckId == id
                                select c).ToList();

            foreach (var c in cards)
            {
                db.Cards.Remove(c);
            }

            Deck deck = db.Decks.Find(id);
            db.Decks.Remove(deck);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ViewCardsFromDeck(int? id)
        {
            var model = db.Decks.Find(id);
            string deckName = db.Decks.Find(id).Name;
            

            ViewBag.Message = deckName;
            ViewBag.DeckId = id;

            return View(model);
        }


        public ActionResult CycleThroughDeck(int? id, int index = 0)
        {
            ViewBag.Id = id;
            var cards = (from c in db.Cards
                         where c.DeckId == id
                         select c).ToArray();

            var max = cards.Count();

            var currentCard = cards[index];

            if (index == 0 && max == 1)
            {
                ViewBag.PreviousIndex = index;
                ViewBag.NextIndex = index;
            }
            else if (index == 0)
            {
                ViewBag.PreviousIndex = max - 1;
                ViewBag.NextIndex = 1;
            }
            else if (index == 0 && max == 1)
            {
                ViewBag.PreviousIndex = index;
                ViewBag.NextIndex = index;
            }
            else if (index == max - 1 && index != 0)
            {
                ViewBag.PreviousIndex = index - 1;
                ViewBag.NextIndex = 0;
            }
            else
            {
                ViewBag.PreviousIndex = index - 1;
                ViewBag.NextIndex = index + 1;
            }

            return View(currentCard);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
