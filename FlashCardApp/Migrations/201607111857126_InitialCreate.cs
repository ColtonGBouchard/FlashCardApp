namespace FlashCardApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Term = c.String(),
                        Definition = c.String(),
                        DeckId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Decks", t => t.DeckId, cascadeDelete: true)
                .Index(t => t.DeckId);
            
            CreateTable(
                "dbo.Decks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "DeckId", "dbo.Decks");
            DropIndex("dbo.Cards", new[] { "DeckId" });
            DropTable("dbo.Decks");
            DropTable("dbo.Cards");
        }
    }
}
