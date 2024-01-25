namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        AdressId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        HomeNumber = c.String(),
                        FlatNumber = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        Email = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdressId);
            
            CreateTable(
                "dbo.Opis",
                c => new
                    {
                        OpisId = c.Int(nullable: false, identity: true),
                        ProduktId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        adderId = c.Int(nullable: false),
                        removeDate = c.DateTime(),
                        removerId = c.Int(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OpisId);
            
            CreateTable(
                "dbo.Globals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kategorias",
                c => new
                    {
                        KategoriaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        PersonAdderId = c.Int(nullable: false),
                        PersonRemoverId = c.Int(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.KategoriaId);
            
            CreateTable(
                "dbo.Komentarzs",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ProduktId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .Index(t => t.ProduktId);
            
            CreateTable(
                "dbo.Logowanies",
                c => new
                    {
                        LogowanieId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.LogowanieId);
            
            CreateTable(
                "dbo.Parametrs",
                c => new
                    {
                        Parametrid = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        jednostka = c.String(),
                        typ_JednostkiId = c.Int(nullable: false),
                        PersonAdderId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        PersonRemoverId = c.Int(nullable: false),
                        removeDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Parametrid);
            
            CreateTable(
                "dbo.Parametr_Produkt",
                c => new
                    {
                        Parametr_ProduktId = c.Int(nullable: false, identity: true),
                        ParametrId = c.Int(nullable: false),
                        ProduktId = c.Int(nullable: false),
                        Value = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Parametr_ProduktId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        LogowanieId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Logowanies", t => t.LogowanieId, cascadeDelete: true)
                .Index(t => t.LogowanieId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        link = c.String(),
                        positionX = c.Int(nullable: false),
                        positionY = c.Int(nullable: false),
                        sizeX = c.Int(nullable: false),
                        sizeY = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        adderId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.Produkts",
                c => new
                    {
                        ProduktId = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Ilosc_w_magazynie = c.Int(nullable: false),
                        rodzaj_miaryId = c.Int(nullable: false),
                        cenaNetto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        vatId = c.Int(nullable: false),
                        glownaWalutaId = c.Int(nullable: false),
                        rodzajId = c.Int(nullable: false),
                        Kupiono_lacznie = c.Int(nullable: false),
                        adderId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        removeDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProduktId);
            
            CreateTable(
                "dbo.Produkty_w_zamowieniu",
                c => new
                    {
                        Produkty_w_zamowieniuId = c.Int(nullable: false, identity: true),
                        zamowienieId = c.Int(nullable: false),
                        ProduktId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Produkty_w_zamowieniuId);
            
            CreateTable(
                "dbo.Rodzajs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        KategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rodzaj_dostawy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rodzaj_miary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rodzaj_platnosci",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        adderId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        removerId = c.Int(),
                        removerDate = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        OpisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SectionId);
            
            CreateTable(
                "dbo.Typ_jednostki",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Typ_danych = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Walutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zamowienias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        zamawiajacyId = c.Int(nullable: false),
                        adresId = c.Int(nullable: false),
                        dostawaId = c.Int(nullable: false),
                        platnosc_typId = c.Int(nullable: false),
                        walutaId = c.Int(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Komentarzs", "ProduktId", "dbo.Produkts");
            DropForeignKey("dbo.People", "LogowanieId", "dbo.Logowanies");
            DropIndex("dbo.People", new[] { "LogowanieId" });
            DropIndex("dbo.Komentarzs", new[] { "ProduktId" });
            DropTable("dbo.Zamowienias");
            DropTable("dbo.Walutas");
            DropTable("dbo.Typ_jednostki");
            DropTable("dbo.Sections");
            DropTable("dbo.Rodzaj_platnosci");
            DropTable("dbo.Rodzaj_miary");
            DropTable("dbo.Rodzaj_dostawy");
            DropTable("dbo.Rodzajs");
            DropTable("dbo.Produkty_w_zamowieniu");
            DropTable("dbo.Produkts");
            DropTable("dbo.Photos");
            DropTable("dbo.People");
            DropTable("dbo.Parametr_Produkt");
            DropTable("dbo.Parametrs");
            DropTable("dbo.Logowanies");
            DropTable("dbo.Komentarzs");
            DropTable("dbo.Kategorias");
            DropTable("dbo.Globals");
            DropTable("dbo.Opis");
            DropTable("dbo.Adresses");
            DropTable("dbo.AccountTypes");
        }
    }
}
