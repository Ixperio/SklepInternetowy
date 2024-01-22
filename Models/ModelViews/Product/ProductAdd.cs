using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sklep.Models.ModelViews.Product
{
    public class ProductAdd
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public double Cena { get; set; }

        public int IloscWMagazynie { get; set; }

    }
}