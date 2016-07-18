using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlashCardApp.Models;

namespace FlashCardApp.Controllers
{
    public class CardController : Controller
    {
        private FlashCardAppDb db = new FlashCardAppDb();

        // GET: /Card/
        public ActionResult Index()
        {
            return View(db.Cards.ToList());
        }

        // GET: /Card/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: /Card/Create
        public ActionResult Create(int? deckId)
        {
            ViewBag.Id = deckId;
            return View();
        }

        // POST: /Card/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Term,Definition,DeckId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Create", "Card", new { deckId = card.DeckId });
            }
            
            return View(card);
        }



        // GET: /Card/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: /Card/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Term,Definition,DeckId")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewCardsFromDeck", "Deck", new { id = card.DeckId });
            }
            return RedirectToAction("ViewCardsFromDeck", "Deck", new { id = card.DeckId });
        }

        // GET: /Card/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            ViewBag.id = card.DeckId;
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: /Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Cards.Find(id);
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("ViewCardsFromDeck", "Deck", new { id = card.DeckId });
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
