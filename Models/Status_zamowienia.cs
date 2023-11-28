using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Status_zamowienia
    {
        public int Id {  get; set; }
        public string Status { get; set; }

        public virtual ICollection<Zamowienia> Zamowienia { get; set; }
    }
}