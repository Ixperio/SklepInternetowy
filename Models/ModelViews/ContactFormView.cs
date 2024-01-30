using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    public class ContactFormView
    {
        [DisplayName("Podaj email kontaktowy")]
        public string email { get; set; }
        [DisplayName("Podaj swoją godność")]
        public string name { get; set; }
        [DisplayName("Wprowadź wiadomość")]
        public string message { get; set; }
    }
}