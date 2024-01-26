using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //Klasa widoku dla Komentarza produktowego - Katarzyna Grygo
    public class ProductComment
    {
        public int ProduktId { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
    }
}