
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string Nazwa { get; set; }
        public virtual ICollection<Opis> opis { get; set; }

        public virtual ICollection<Parametr_Produkt> parametr { get; set; }
        public int Ilosc_w_magazynie {  get; set; }

        public int rodzaj_miaryId { get; set; }
        public virtual Rodzaj_miary rodzaj_miary { get; set; }
        public decimal cena { get; set; }

        public int glownaWalutaId { get; set; }
        public virtual Waluta glownaWlauta { get; set; }

        public int rodzajId { get; set; }
        public virtual Rodzaj rodzaj { get; set; }
        public int Kupiono_lacznie { get; set; } //z uwagi na mniejsze obciązanie bazy danych

        public int adderId { get; set; }
        public virtual Person adder { get; set; }

        public bool isDeleted { get; set; } = false;
        public bool isVisible { get; set; } = false;

        public DateTime addDate { get; set; } = DateTime.Now;
        public DateTime? removeDate { get; set; }

        public virtual ICollection<Produkty_w_zamowieniu> produkty { get; set; }

    }
}