using System.Data.Entity;

namespace FlashCardApp.Models
{
    public class FlashCardAppDb : DbContext
    {
        public FlashCardAppDb() : base("name=DefaultConnection")
        {

        }

        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
    }
}