namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zamowienias", "status", c => c.String());
            DropColumn("dbo.Zamowienias", "statusId");
            DropTable("dbo.Status_zamowienia");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status_zamowienia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Zamowienias", "statusId", c => c.Int(nullable: false));
            DropColumn("dbo.Zamowienias", "status");
        }
    }
}
