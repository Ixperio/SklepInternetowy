
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Rodzaj
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int KategoriaId { get; set; }
        public virtual ICollection<Kategoria> Kategoria { get; set; }
        public virtual ICollection<Produkt> Produkt { get; set; }
    }
}