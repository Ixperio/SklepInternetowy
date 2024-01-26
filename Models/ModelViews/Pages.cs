using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //Model zawierający dane o aktualnie wyświetlanej stronie z produktami oraz jaki jest limit produktów wyświetlanych na jednej stronie.
    //Artur Leszczak
    public class Pages
    {
        public int Page {  get; set; }
        public int PageLimit { get; set; }
    }
}