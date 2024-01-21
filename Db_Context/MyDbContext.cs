using Sklep.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection.Emit;

namespace Sklep.Db_Context
{
    public sealed class MyDbContext : DbContext
    {
        private static MyDbContext _instance;
        private MyDbContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sklep;Integrated Security=True;Connect Timeout=30;Encrypt=False;")
        {

        }

        public static MyDbContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MyDbContext();
            }
            return _instance;
        }

        public DbSet<Logowanie> Logowanie { get; set; }

        public DbSet<Produkt> Products { get; set; }

        public DbSet<AccountType> Account_type { get; set; }

        public DbSet<Adress> Adress { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Kategoria> Kategoria { get; set; }

        public DbSet<Opis> Description { get; set; }

        public DbSet<Parametr> Parametr { get; set; }

        public DbSet<Parametr_Produkt> Parametr_w_produkcie { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Photo> Photo { get; set; }

        public DbSet<Produkty_w_zamowieniu> Produkty_w_zamowieniu { get; set; }

        public DbSet<Rodzaj> Rodzaj { get; set; }

        public DbSet<Rodzaj_dostawy> Rodzaj_dostawy { get; set; }
        public DbSet<Rodzaj_miary> Rodzaj_miary { get; set; }

        public DbSet<Rodzaj_platnosci> Rodzaj_platnosci { get; set; }

        public DbSet<Section> Sekcja { get; set; }

        public DbSet<Status_zamowienia> Status_zamowienia { get; set; }

        public DbSet<Town> Miasto { get; set; }

        public DbSet<Typ_jednostki> Typ_jednostki { get; set; }

        public DbSet<Waluta> Waluta { get; set; }
        
        public DbSet<Zamowienia> Zamowienia { get; set; }
       

    }
}