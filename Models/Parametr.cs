using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Parametr
    {
        public int Parametrid {  get; set; }
        public string name { get; set; }

        public string jednostka { get; set; }

        public int typ_JednostkiId { get; set; }
        public virtual ICollection<Typ_jednostki> typ_Jednostki { get; set; }

        public int PersonAdderId { get; set; }
        public virtual Person PersonAdder { get; set; }

        public DateTime addDate { get; set; }

        public int PersonRemoverId { get; set; }
        public virtual Person? PersonRemover { get; set; }
        public DateTime? removeDate { get; set; }

        public virtual ICollection<Parametr_Produkt> parametr { get; set;}

    }
}