
﻿using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
﻿using Sklep.Models;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection.Emit;

namespace Sklep.Db_Context
{
    public sealed class MyDbContext : DbContext
    {
        private static MyDbContext _instance; //prywatna instancja singletona

        //ŚCIEŻKA DO PLIKU BAZY DANYCH (W App_Data)

        private static string DB_PATH = "C:\\Users\\alok1\\Desktop\\STUDIA\\SEM5\\MVC\\PROJEKT\\Sklep\\App_Data\\Baza.mdf";

        [Obsolete("Konstruktor do ef")]
        public MyDbContext() : base("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename="+DB_PATH+";Integrated Security=True;Connect Timeout=30;Encrypt=False;")
        {
        }
        private MyDbContext(bool x) : base("Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename="+DB_PATH+";Integrated Security=True;Connect Timeout=30;Encrypt=False;")
        {
        }
        //SINGLETON - Katarzyna Grygo
        //zwraca instniejącą instancje lub tworzy instancję połączenia jeżeli brak takowej
        public static MyDbContext GetInstance()
        {
            
            if (_instance == null)
            {
                _instance = new MyDbContext(true);
            }
           
            return _instance;
        }
        //KONFIGURACJA MAPOWANIA OBIEKTOWEGO
        public DbSet<Logowanie> Logowanie { get; set; }

        public DbSet<Produkt> Products { get; set; }

        public DbSet<AccountType> Account_type { get; set; }

        public DbSet<Adress> Adress { get; set; }

        public DbSet<Kategoria> Kategoria { get; set; }

        public DbSet<Komentarz> Komentarze { get; set; }
        public DbSet<Opis> Description { get; set; }

        public DbSet<Parametr> Parametr { get; set; }

        public DbSet<Parametr_Produkt> Parametr_w_produkcie { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Photo> Photo { get; set; }
        public DbSet<Podatek> Podatek { get; set; }
        public DbSet<Produkty_w_zamowieniu> Produkty_w_zamowieniu { get; set; }

        public DbSet<Produkty_w_zamowieniu_goscie> Produkty_w_zamowieniu_goscie { get; set; }
        public DbSet<Rodzaj> Rodzaj { get; set; }

        public DbSet<Rodzaj_dostawy> Rodzaj_dostawy { get; set; }
        public DbSet<Rodzaj_miary> Rodzaj_miary { get; set; }

        public DbSet<Rodzaj_platnosci> Rodzaj_platnosci { get; set; }

        public DbSet<Section> Sekcja { get; set; }

        public DbSet<Typ_jednostki> Typ_jednostki { get; set; }

        public DbSet<Waluta> Waluta { get; set; }
        
        public DbSet<ZamowieniaKlienci> Zamowienia { get; set; }

        public DbSet<ZamowieniaGoscie> ZamowieniaGoscie { get; set; }
        public  DbSet<Globals> Globals { get; set; }

        public DbSet<Zamowienie_status> Zamowienie_status { get; set; }

        public DbSet<Promocja> Promocja { get; set;}

        public DbSet<Promocja_produkt> Promocja_produkt { get; set; }

    }
}