using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class ContactFormExpertView
    {
        public string email { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public int produktId { get; set; }
    }
}