namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkts", "opis_OpisId", c => c.Int());
            CreateIndex("dbo.Produkts", "opis_OpisId");
            AddForeignKey("dbo.Produkts", "opis_OpisId", "dbo.Opis", "OpisId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Produkts", "opis_OpisId", "dbo.Opis");
            DropIndex("dbo.Produkts", new[] { "opis_OpisId" });
            DropColumn("dbo.Produkts", "opis_OpisId");
        }
    }
}
