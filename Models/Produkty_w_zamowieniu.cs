
using System.Collections;
using System.Collections.Generic;

namespace Sklep.Models
{
    public class Produkty_w_zamowieniu
    {
        public int Produkty_w_zamowieniuId { get; set; }
        public int zamowienieId { get; set; }
        public virtual Zamowienia zamowienie { get; set; }

        public int ProduktId { get; set; }
        public virtual Produkt Produkt { get; set; }
        public uint Liczba { get; set; }

    }
}
