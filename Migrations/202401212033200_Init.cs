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
                        TownID = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        removeDate = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdressId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.CountryId);
            
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
                    })
                .PrimaryKey(t => t.OpisId);
            
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
                "dbo.Logowanies",
                c => new
                    {
                        LogowanieId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.LogowanieId);
            
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
                .PrimaryKey(t => t.PersonId);
            
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
                "dbo.Status_zamowienia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        statusId = c.Int(nullable: false),
                        walutaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Zamowienias");
            DropTable("dbo.Walutas");
            DropTable("dbo.Typ_jednostki");
            DropTable("dbo.Status_zamowienia");
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
            DropTable("dbo.Towns");
            DropTable("dbo.Logowanies");
            DropTable("dbo.Kategorias");
            DropTable("dbo.Opis");
            DropTable("dbo.Countries");
            DropTable("dbo.Adresses");
            DropTable("dbo.AccountTypes");
        }
    }
}
