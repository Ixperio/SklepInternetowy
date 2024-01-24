namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Adresses", "addDate");
            DropColumn("dbo.Adresses", "removeDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Adresses", "removeDate", c => c.DateTime());
            AddColumn("dbo.Adresses", "addDate", c => c.DateTime(nullable: false));
        }
    }
}
