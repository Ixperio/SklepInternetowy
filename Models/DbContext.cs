using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Reflection.Emit;

namespace Sklep.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\alok1\\Desktop\\Studia\\SklepInternetowy\\App_Data\\DB.mdf;Integrated Security=True")
        {

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