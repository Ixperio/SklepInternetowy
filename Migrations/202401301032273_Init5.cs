namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Password");
        }
    }
}
