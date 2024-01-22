using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Metoda_wytworcza
{
    public class Kurier : IDostawa
    {
        private string name;
        private decimal price;

        public Kurier(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public string getName() { return name; }

        public decimal getPrice() { return price; }

    }
}