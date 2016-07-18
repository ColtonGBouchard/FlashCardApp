using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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