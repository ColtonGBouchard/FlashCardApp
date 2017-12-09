using System.Collections.Generic;

namespace FlashCardApp.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<Card> Flashcards { get; set; }
    }
}