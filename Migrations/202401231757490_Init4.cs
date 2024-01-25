namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Globals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Photos", "ProductId", c => c.Int(nullable: false));
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Globals");
            DropColumn("dbo.Photos", "ProductId");
        }
    }
}
