using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Adress
    {
        public int AdressId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }

        public int TownID { get; set; }

        public DateTime addDate { get; set; }
        public DateTime? removeDate { get; set; }
        public bool isDeleted { get; set; }

        public int PersonId { get; set; }

    }
}