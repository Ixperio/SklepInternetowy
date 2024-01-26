namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promocjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Promocja_produkt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProduktId = c.Int(nullable: false),
                        PromocjaId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        removeDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Promocja_produkt");
            DropTable("dbo.Promocjas");
        }
    }
}
