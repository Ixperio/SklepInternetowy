
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sklep.Models
{
    public class Produkty_w_zamowieniu_goscie
    {
        [Key]
        public int Produkty_w_zamowieniuId { get; set; }
        public int zamowienie_goscieId { get; set; }
        public int ProduktId { get; set; }
        public uint Liczba { get; set; }

    }
}
