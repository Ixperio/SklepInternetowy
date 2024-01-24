namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Komentarzs",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ProduktId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .Index(t => t.ProduktId);
            CreateTable(
               "dbo.Globals",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Name = c.String(),
                   Value = c.Int(nullable: false),
               })
               .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Komentarzs", "ProduktId", "dbo.Produkts");
            DropIndex("dbo.Komentarzs", new[] { "ProduktId" });
            DropTable("dbo.Komentarzs");
            DropTable("dbo.Globals");
        }
    }
}
