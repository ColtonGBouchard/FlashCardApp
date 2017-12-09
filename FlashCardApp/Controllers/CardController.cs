using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FlashCardApp.Models;

namespace FlashCardApp.Controllers
{
    public class CardController : Controller
    {
        private FlashCardAppDb _flashCardAppDb = new FlashCardAppDb();

        // GET: /Card/
        public ActionResult Index()
        {
            return View(_flashCardAppDb.Cards.ToList());
        }

        // GET: /Card/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var card = _flashCardAppDb.Cards.Find(id);

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
            if (!ModelState.IsValid) return View(card);

            _flashCardAppDb.Cards.Add(card);
            _flashCardAppDb.SaveChanges();

            return RedirectToAction("Create", "Card", new { deckId = card.DeckId });
        }

        // GET: /Card/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var card = _flashCardAppDb.Cards.Find(id);

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
            if (!ModelState.IsValid) return RedirectToAction("ViewCardsFromDeck", "Deck", new {id = card.DeckId});

            _flashCardAppDb.Entry(card).State = EntityState.Modified;

            _flashCardAppDb.SaveChanges();

            return RedirectToAction("ViewCardsFromDeck", "Deck", new { id = card.DeckId });
        }

        // GET: /Card/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var card = _flashCardAppDb.Cards.Find(id);
            ViewBag.id = card.DeckId;

            return View(card);
        }

        // POST: /Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var card = _flashCardAppDb.Cards.Find(id);

            _flashCardAppDb.Cards.Remove(card);
            _flashCardAppDb.SaveChanges();

            return RedirectToAction("ViewCardsFromDeck", "Deck", new { id = card.DeckId });
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
