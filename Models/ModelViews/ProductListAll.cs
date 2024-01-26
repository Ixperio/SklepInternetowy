using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //Klasa widoku pozwalająca na przekazywanie jedynie niezbędnych danych do widoku - Artur Leszczak
    public class ProductListAll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public decimal NettoPrice { get; set; }
        public decimal BruttoPrice { get; set; }
        public int OpinionCounter { get; set; }
        public decimal OpinionValue { get; set; }
        public int StoreCount { get; set; }
    }
}