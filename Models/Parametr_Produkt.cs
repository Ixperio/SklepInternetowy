using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Parametr_Produkt
    {
        public int Parametr_ProduktId { get; set; }

        public int parametrId { get; set; }
        public virtual Parametr parametr { get; set; }

        public int ProduktId { get; set; }
        public virtual Produkt Produkt { get; set; }
        public string Value { get; set; }
        public bool isDeleted { get; set; }
        public bool isVisible { get; set; }

        
    }
}