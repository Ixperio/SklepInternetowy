namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adresses", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adresses", "Email");
        }
    }
}
