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
        public string FlatNumber { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public bool isDeleted { get; set; }

        public int PersonId { get; set; }

    }
}