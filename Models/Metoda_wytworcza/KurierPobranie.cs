using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sklep.Models.Metoda_wytworcza.Interface;

namespace Sklep.Models.Metoda_wytworcza
{
    public class KurierPobranie : IDostawa
    {
        private string name;
        private decimal price;

        public KurierPobranie(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public string getName() { return name; }

        public decimal getPrice() { return price; }
    }
}