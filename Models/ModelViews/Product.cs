using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sklep.Models.Interfaces;


namespace Sklep.Models.ModelViews
{
    public class Product : IProducts
    {
        private decimal Price { get; set;}
        public Product(decimal Price)
        {
            this.Price = Price;
        }
        
        public decimal getPrice()
        {
            return this.Price;
        }

    }
}