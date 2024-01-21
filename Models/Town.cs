
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Town
    {
        public int TownId { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public int CountryId { get; set; }
        public bool isDeleted { get; set; }

    }
}