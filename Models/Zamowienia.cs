
using System.Collections.Generic;

namespace Sklep.Models
{
    public class Zamowienia
    {
        public int Id { get; set; }

        public int zamawiajacyId { get; set; }
        public virtual Person zamawiajacy { get; set; }

        public virtual ICollection<Produkty_w_zamowieniu> produkty { get; set; }

        public int adresId { get; set; }
        public virtual Adress adres { get; set; }

        public int dostawaId { get; set; }
        public virtual Rodzaj_dostawy dostawa { get; set; }
        public int platnosc_typId { get; set; }
        public virtual Rodzaj_platnosci platnosc_typ { get; set; }
        public int statusId { get; set; }
        public virtual Status_zamowienia status { get; set; }

        public int walutaId { get; set; }
        public virtual Waluta waluta { get; set; }

    }
}
