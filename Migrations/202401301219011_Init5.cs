namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Password", c => c.String());
            AddColumn("dbo.People", "ResetToken", c => c.String());
            DropTable("dbo.ConfigDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ConfigDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Description = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.People", "ResetToken");
            DropColumn("dbo.People", "Password");
        }
    }
}
