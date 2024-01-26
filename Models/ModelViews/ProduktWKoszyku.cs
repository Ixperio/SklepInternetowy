using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //klasa widoku reprezentująca model Produktu w koszyku - Artur Leszczak
    public class ProduktWKoszyku
    {
        public Produkt Produkt { get; set; }
        public string Icon { get; set; }
        public int Ilosc { get; set; }
        public decimal StawkaVat { get; set; }
        public decimal CenaBrutto { get; set; }
        public decimal Wartosc { get; set; }
    }
}