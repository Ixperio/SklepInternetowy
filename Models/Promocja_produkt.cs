using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Promocja_produkt
    {
        public int Id { get; set; }
        public int ProduktId { get; set; }
        public int PromocjaId { get; set; }
        public DateTime addDate { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? removeDate { get; set; }


    }
}