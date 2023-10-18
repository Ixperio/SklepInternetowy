using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepInternetowy.Models
{
    public class Language
    {

        public string Name {  get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }

        public void SetPolish() {
            this.Name = "Imie";
            this.Surname = "Nazwisko";
            this.Email = "Adres Email";
        }

        public void SetEngilsh()
        {
            this.Name = "Name";
            this.Surname = "Surname";
            this.Email = "Email address";
        }

    }
}