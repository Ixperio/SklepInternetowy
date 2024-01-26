using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Waluta
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
    }
}