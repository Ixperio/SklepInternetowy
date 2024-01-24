using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Produkt
    {
        public int ProduktId { get; set; }
        public string Nazwa { get; set; }
        public int Ilosc_w_magazynie {  get; set; }

        public int rodzaj_miaryId { get; set; }
        public decimal cenaNetto { get; set; }

        public Opis opis { get; set; }
        public int vatId { get; set; }

        public int glownaWalutaId { get; set; }

        public int rodzajId { get; set; }
        public int Kupiono_lacznie { get; set; } //z uwagi na mniejsze obciązanie bazy danych

        public int adderId { get; set; }

        public bool isDeleted { get; set; } = false;
        public bool isVisible { get; set; } = false;

        public DateTime addDate { get; set; } = DateTime.Now;
        public DateTime? removeDate { get; set; }

        public virtual ICollection<Komentarz> Komentarze { get; set; }
    }
}