namespace Sklep.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init2 : DbMigration
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
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Logowanies", t => t.LogowanieId, cascadeDelete: true)
                .Index(t => t.LogowanieId)
                .Index(t => t.AccountTypeId);
            
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
                .PrimaryKey(t => t.AdressId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Towns", t => t.TownID, cascadeDelete: true)
                .Index(t => t.TownID)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        TownId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostCode = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TownId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.Country_CountryId);
            
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
                        adres_AdressId = c.Int(),
                        zamawiajacy_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.adres_AdressId)
                .ForeignKey("dbo.Rodzaj_dostawy", t => t.dostawaId, cascadeDelete: true)
                .ForeignKey("dbo.Rodzaj_platnosci", t => t.platnosc_typId, cascadeDelete: true)
                .ForeignKey("dbo.Status_zamowienia", t => t.statusId, cascadeDelete: true)
                .ForeignKey("dbo.Walutas", t => t.walutaId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.zamawiajacy_PersonId)
                .Index(t => t.dostawaId)
                .Index(t => t.platnosc_typId)
                .Index(t => t.statusId)
                .Index(t => t.walutaId)
                .Index(t => t.adres_AdressId)
                .Index(t => t.zamawiajacy_PersonId);
            
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
                "dbo.Produkty_w_zamowieniu",
                c => new
                    {
                        Produkty_w_zamowieniuId = c.Int(nullable: false, identity: true),
                        zamowienieId = c.Int(nullable: false),
                        ProduktId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Produkty_w_zamowieniuId)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .ForeignKey("dbo.Zamowienias", t => t.zamowienieId, cascadeDelete: true)
                .Index(t => t.zamowienieId)
                .Index(t => t.ProduktId);
            
            CreateTable(
                "dbo.Produkts",
                c => new
                    {
                        ProduktId = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Ilosc_w_magazynie = c.Int(nullable: false),
                        rodzaj_miaryId = c.Int(nullable: false),
                        cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        glownaWalutaId = c.Int(nullable: false),
                        rodzajId = c.Int(nullable: false),
                        Kupiono_lacznie = c.Int(nullable: false),
                        adderId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        removeDate = c.DateTime(),
                        adder_PersonId = c.Int(),
                        glownaWlauta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ProduktId)
                .ForeignKey("dbo.People", t => t.adder_PersonId)
                .ForeignKey("dbo.Walutas", t => t.glownaWlauta_Id)
                .ForeignKey("dbo.Rodzajs", t => t.rodzajId, cascadeDelete: true)
                .ForeignKey("dbo.Rodzaj_miary", t => t.rodzaj_miaryId, cascadeDelete: true)
                .Index(t => t.rodzaj_miaryId)
                .Index(t => t.rodzajId)
                .Index(t => t.adder_PersonId)
                .Index(t => t.glownaWlauta_Id);
            
            CreateTable(
                "dbo.Walutas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Waluta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Walutas", t => t.Waluta_Id)
                .Index(t => t.Waluta_Id);
            
            CreateTable(
                "dbo.Opis",
                c => new
                    {
                        OpisId = c.Int(nullable: false, identity: true),
                        ProduktId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        adderId = c.Int(nullable: false),
                        removeDate = c.DateTime(),
                        removeerId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        adder_PersonId = c.Int(),
                        remover_PersonId = c.Int(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.OpisId)
                .ForeignKey("dbo.People", t => t.adder_PersonId)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.remover_PersonId)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.ProduktId)
                .Index(t => t.adder_PersonId)
                .Index(t => t.remover_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        adderId = c.Int(nullable: false),
                        addDate = c.DateTime(nullable: false),
                        removerId = c.Int(nullable: false),
                        removerDate = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        OpisId = c.Int(nullable: false),
                        adder_PersonId = c.Int(),
                        remover_PersonId = c.Int(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.People", t => t.adder_PersonId)
                .ForeignKey("dbo.Opis", t => t.OpisId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.remover_PersonId)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.OpisId)
                .Index(t => t.adder_PersonId)
                .Index(t => t.remover_PersonId)
                .Index(t => t.Person_PersonId);
            
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
                        adder_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.People", t => t.adder_PersonId)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.adder_PersonId);
            
            CreateTable(
                "dbo.Parametr_Produkt",
                c => new
                    {
                        Parametr_ProduktId = c.Int(nullable: false, identity: true),
                        parametrId = c.Int(nullable: false),
                        ProduktId = c.Int(nullable: false),
                        Value = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Parametr_ProduktId)
                .ForeignKey("dbo.Parametrs", t => t.parametrId, cascadeDelete: true)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .Index(t => t.parametrId)
                .Index(t => t.ProduktId);
            
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
                        PersonAdder_PersonId = c.Int(),
                        PersonRemover_PersonId = c.Int(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.Parametrid)
                .ForeignKey("dbo.People", t => t.PersonAdder_PersonId)
                .ForeignKey("dbo.People", t => t.PersonRemover_PersonId)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.PersonAdder_PersonId)
                .Index(t => t.PersonRemover_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Typ_jednostki",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Typ_danych = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        isVisible = c.Boolean(nullable: false),
                        ProduktId = c.Int(nullable: false),
                        Parametr_Parametrid = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Produkts", t => t.ProduktId, cascadeDelete: true)
                .ForeignKey("dbo.Parametrs", t => t.Parametr_Parametrid)
                .Index(t => t.ProduktId)
                .Index(t => t.Parametr_Parametrid);
            
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
                        RodzajId = c.Int(nullable: false),
                        PersonAdder_PersonId = c.Int(),
                        PersonRemover_PersonId = c.Int(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.KategoriaId)
                .ForeignKey("dbo.People", t => t.PersonAdder_PersonId)
                .ForeignKey("dbo.People", t => t.PersonRemover_PersonId)
                .ForeignKey("dbo.Rodzajs", t => t.RodzajId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.RodzajId)
                .Index(t => t.PersonAdder_PersonId)
                .Index(t => t.PersonRemover_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Rodzaj_miary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status_zamowienia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logowanies",
                c => new
                    {
                        LogowanieId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.LogowanieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.Parametrs", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.Opis", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.People", "LogowanieId", "dbo.Logowanies");
            DropForeignKey("dbo.Kategorias", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.Zamowienias", "zamawiajacy_PersonId", "dbo.People");
            DropForeignKey("dbo.Zamowienias", "walutaId", "dbo.Walutas");
            DropForeignKey("dbo.Zamowienias", "statusId", "dbo.Status_zamowienia");
            DropForeignKey("dbo.Produkty_w_zamowieniu", "zamowienieId", "dbo.Zamowienias");
            DropForeignKey("dbo.Produkts", "rodzaj_miaryId", "dbo.Rodzaj_miary");
            DropForeignKey("dbo.Produkts", "rodzajId", "dbo.Rodzajs");
            DropForeignKey("dbo.Kategorias", "RodzajId", "dbo.Rodzajs");
            DropForeignKey("dbo.Kategorias", "PersonRemover_PersonId", "dbo.People");
            DropForeignKey("dbo.Kategorias", "PersonAdder_PersonId", "dbo.People");
            DropForeignKey("dbo.Produkty_w_zamowieniu", "ProduktId", "dbo.Produkts");
            DropForeignKey("dbo.Parametr_Produkt", "ProduktId", "dbo.Produkts");
            DropForeignKey("dbo.Typ_jednostki", "Parametr_Parametrid", "dbo.Parametrs");
            DropForeignKey("dbo.Typ_jednostki", "ProduktId", "dbo.Produkts");
            DropForeignKey("dbo.Parametrs", "PersonRemover_PersonId", "dbo.People");
            DropForeignKey("dbo.Parametrs", "PersonAdder_PersonId", "dbo.People");
            DropForeignKey("dbo.Parametr_Produkt", "parametrId", "dbo.Parametrs");
            DropForeignKey("dbo.Sections", "remover_PersonId", "dbo.People");
            DropForeignKey("dbo.Photos", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Photos", "adder_PersonId", "dbo.People");
            DropForeignKey("dbo.Sections", "OpisId", "dbo.Opis");
            DropForeignKey("dbo.Sections", "adder_PersonId", "dbo.People");
            DropForeignKey("dbo.Opis", "remover_PersonId", "dbo.People");
            DropForeignKey("dbo.Opis", "ProduktId", "dbo.Produkts");
            DropForeignKey("dbo.Opis", "adder_PersonId", "dbo.People");
            DropForeignKey("dbo.Walutas", "Waluta_Id", "dbo.Walutas");
            DropForeignKey("dbo.Produkts", "glownaWlauta_Id", "dbo.Walutas");
            DropForeignKey("dbo.Produkts", "adder_PersonId", "dbo.People");
            DropForeignKey("dbo.Zamowienias", "platnosc_typId", "dbo.Rodzaj_platnosci");
            DropForeignKey("dbo.Zamowienias", "dostawaId", "dbo.Rodzaj_dostawy");
            DropForeignKey("dbo.Zamowienias", "adres_AdressId", "dbo.Adresses");
            DropForeignKey("dbo.Towns", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Adresses", "TownID", "dbo.Towns");
            DropForeignKey("dbo.Adresses", "PersonId", "dbo.People");
            DropForeignKey("dbo.People", "AccountTypeId", "dbo.AccountTypes");
            DropIndex("dbo.Kategorias", new[] { "Person_PersonId" });
            DropIndex("dbo.Kategorias", new[] { "PersonRemover_PersonId" });
            DropIndex("dbo.Kategorias", new[] { "PersonAdder_PersonId" });
            DropIndex("dbo.Kategorias", new[] { "RodzajId" });
            DropIndex("dbo.Typ_jednostki", new[] { "Parametr_Parametrid" });
            DropIndex("dbo.Typ_jednostki", new[] { "ProduktId" });
            DropIndex("dbo.Parametrs", new[] { "Person_PersonId" });
            DropIndex("dbo.Parametrs", new[] { "PersonRemover_PersonId" });
            DropIndex("dbo.Parametrs", new[] { "PersonAdder_PersonId" });
            DropIndex("dbo.Parametr_Produkt", new[] { "ProduktId" });
            DropIndex("dbo.Parametr_Produkt", new[] { "parametrId" });
            DropIndex("dbo.Photos", new[] { "adder_PersonId" });
            DropIndex("dbo.Photos", new[] { "SectionId" });
            DropIndex("dbo.Sections", new[] { "Person_PersonId" });
            DropIndex("dbo.Sections", new[] { "remover_PersonId" });
            DropIndex("dbo.Sections", new[] { "adder_PersonId" });
            DropIndex("dbo.Sections", new[] { "OpisId" });
            DropIndex("dbo.Opis", new[] { "Person_PersonId" });
            DropIndex("dbo.Opis", new[] { "remover_PersonId" });
            DropIndex("dbo.Opis", new[] { "adder_PersonId" });
            DropIndex("dbo.Opis", new[] { "ProduktId" });
            DropIndex("dbo.Walutas", new[] { "Waluta_Id" });
            DropIndex("dbo.Produkts", new[] { "glownaWlauta_Id" });
            DropIndex("dbo.Produkts", new[] { "adder_PersonId" });
            DropIndex("dbo.Produkts", new[] { "rodzajId" });
            DropIndex("dbo.Produkts", new[] { "rodzaj_miaryId" });
            DropIndex("dbo.Produkty_w_zamowieniu", new[] { "ProduktId" });
            DropIndex("dbo.Produkty_w_zamowieniu", new[] { "zamowienieId" });
            DropIndex("dbo.Zamowienias", new[] { "zamawiajacy_PersonId" });
            DropIndex("dbo.Zamowienias", new[] { "adres_AdressId" });
            DropIndex("dbo.Zamowienias", new[] { "walutaId" });
            DropIndex("dbo.Zamowienias", new[] { "statusId" });
            DropIndex("dbo.Zamowienias", new[] { "platnosc_typId" });
            DropIndex("dbo.Zamowienias", new[] { "dostawaId" });
            DropIndex("dbo.Countries", new[] { "Country_CountryId" });
            DropIndex("dbo.Towns", new[] { "CountryId" });
            DropIndex("dbo.Adresses", new[] { "PersonId" });
            DropIndex("dbo.Adresses", new[] { "TownID" });
            DropIndex("dbo.People", new[] { "AccountTypeId" });
            DropIndex("dbo.People", new[] { "LogowanieId" });
            DropTable("dbo.Logowanies");
            DropTable("dbo.Status_zamowienia");
            DropTable("dbo.Rodzaj_miary");
            DropTable("dbo.Kategorias");
            DropTable("dbo.Rodzajs");
            DropTable("dbo.Typ_jednostki");
            DropTable("dbo.Parametrs");
            DropTable("dbo.Parametr_Produkt");
            DropTable("dbo.Photos");
            DropTable("dbo.Sections");
            DropTable("dbo.Opis");
            DropTable("dbo.Walutas");
            DropTable("dbo.Produkts");
            DropTable("dbo.Produkty_w_zamowieniu");
            DropTable("dbo.Rodzaj_platnosci");
            DropTable("dbo.Rodzaj_dostawy");
            DropTable("dbo.Zamowienias");
            DropTable("dbo.Countries");
            DropTable("dbo.Towns");
            DropTable("dbo.Adresses");
            DropTable("dbo.People");
            DropTable("dbo.AccountTypes");
        }
    }
}
