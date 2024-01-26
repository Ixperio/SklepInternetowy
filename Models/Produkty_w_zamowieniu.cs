
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sklep.Models
{
    public class Produkty_w_zamowieniu
    {
        [Key]
        public int Produkty_w_zamowieniuId { get; set; }
        public int zamowienieId { get; set; }
        public int ProduktId { get; set; }
        public int ilosc { get; set; }

    }
}
