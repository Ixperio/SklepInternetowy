using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Waluta
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Produkt> Produkt { get; set; }
        public virtual ICollection<Waluta> waluta { get; set; }
    }
}