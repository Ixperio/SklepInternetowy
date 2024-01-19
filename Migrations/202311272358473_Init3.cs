namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Countries", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Countries", new[] { "Country_CountryId" });
            DropColumn("dbo.Countries", "Country_CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "Country_CountryId", c => c.Int());
            CreateIndex("dbo.Countries", "Country_CountryId");
            AddForeignKey("dbo.Countries", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
