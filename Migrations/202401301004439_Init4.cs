namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "ResetToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "ResetToken");
        }
    }
}
