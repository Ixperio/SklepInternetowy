
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklep.Models
{
    public class Zamowienia
    {
        public int Id { get; set; }
        public int zamawiajacyId { get; set; }
        public int adresId { get; set; }
        public int dostawaId { get; set; }
        public int platnosc_typId { get; set; }
        public int statusId { get; set; }
        public int walutaId { get; set; }

    }
}
