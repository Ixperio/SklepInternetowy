namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.People", "LogowanieId");
            AddForeignKey("dbo.People", "LogowanieId", "dbo.Logowanies", "LogowanieId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "LogowanieId", "dbo.Logowanies");
            DropIndex("dbo.People", new[] { "LogowanieId" });
        }
    }
}
