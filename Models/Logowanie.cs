using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep.Models
{
    public class Logowanie
    {
        public int LogowanieId { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }

    }
}