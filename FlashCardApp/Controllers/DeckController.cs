using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FlashCardApp.Models;
using Microsoft.AspNet.Identity;

namespace FlashCardApp.Controllers
{
    public class DeckController : Controller
    {
        FlashCardAppDb _flashCardAppDb = new FlashCardAppDb();

        [Authorize]
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();

            var model = (from d in _flashCardAppDb.Decks
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

            if (!ModelState.IsValid) return View(deck);

            _flashCardAppDb.Decks.Add(deck);
            _flashCardAppDb.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deck = _flashCardAppDb.Decks.Find(id);

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
                _flashCardAppDb.Entry(deck).State = EntityState.Modified;
                _flashCardAppDb.SaveChanges();
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

            var deck = _flashCardAppDb.Decks.Find(id);

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
            var cards = (from c in _flashCardAppDb.Cards
                         where c.DeckId == id
                         select c).ToList();

            foreach (var c in cards)
            {
                _flashCardAppDb.Cards.Remove(c);
            }

            var deck = _flashCardAppDb.Decks.Find(id);

            _flashCardAppDb.Decks.Remove(deck);
            _flashCardAppDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ViewCardsFromDeck(int? id)
        {
            var model = _flashCardAppDb.Decks.Find(id);
            string deckName = _flashCardAppDb.Decks.Find(id).Name;
            

            ViewBag.Message = deckName;
            ViewBag.DeckId = id;

            return View(model);
        }

        public ActionResult CycleThroughDeck(int? id, int index = 0)
        {
            var cards = (from c in _flashCardAppDb.Cards
                         where c.DeckId == id
                         select c).ToArray();

            var max = cards.Count;

            if (max == 0)
            {
                TempData["notice"] = "There are no cards in that deck to study. Please add cards and try again.";
                return RedirectToAction("Index");
            }

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
                _flashCardAppDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
