using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Typ_jednostki
    {
        public int id { get; set; }
        public string Typ_danych { get; set; }
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }

        public int ProduktId { get; set; }
        public virtual Produkt Produkt { get; set; }
    }
}