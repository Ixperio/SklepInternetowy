namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adresses", "FlatNumber", c => c.String());
            AddColumn("dbo.Adresses", "City", c => c.String());
            AddColumn("dbo.Adresses", "PostCode", c => c.String());
            AddColumn("dbo.Adresses", "Country", c => c.String());
            AddColumn("dbo.Adresses", "State", c => c.String());
            DropColumn("dbo.Adresses", "TownID");
            DropTable("dbo.Countries");
            DropTable("dbo.Towns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        TownId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostCode = c.String(),
                        CountryId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TownId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.CountryId);
            
            AddColumn("dbo.Adresses", "TownID", c => c.Int(nullable: false));
            DropColumn("dbo.Adresses", "State");
            DropColumn("dbo.Adresses", "Country");
            DropColumn("dbo.Adresses", "PostCode");
            DropColumn("dbo.Adresses", "City");
            DropColumn("dbo.Adresses", "FlatNumber");
        }
    }
}
