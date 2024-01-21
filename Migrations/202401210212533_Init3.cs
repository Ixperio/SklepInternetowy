namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkts", "cenaNetto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Produkts", "vatId", c => c.Int(nullable: false));
            AlterColumn("dbo.Opis", "removerId", c => c.Int());
            AlterColumn("dbo.Sections", "removerId", c => c.Int());
            DropColumn("dbo.Produkts", "cena");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produkts", "cena", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Sections", "removerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Opis", "removerId", c => c.Int(nullable: false));
            DropColumn("dbo.Produkts", "vatId");
            DropColumn("dbo.Produkts", "cenaNetto");
        }
    }
}
