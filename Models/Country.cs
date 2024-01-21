using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        [MaxLength(3)]
        [MinLength(2)]
        public string Code { get; set; }
    }
}