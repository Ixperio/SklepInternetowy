using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Models
{
    public interface IProductBuilder
    {
        void BuildName(string name);

        void BuildPrice(decimal price);

        void BuildAmount(int amount);
    }

    public class ProductBuilder : IProductBuilder
    {
        private Produkt produkt = new Produkt();

        public ProductBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            produkt = new Produkt();
        }

        public void BuildAmount(int amount)
        {
            produkt.Ilosc_w_magazynie = amount;
        }

        public void BuildName(string name)
        {
            produkt.Nazwa = name;
        }

        public void BuildPrice(decimal price)
        {
            produkt.cenaNetto = price;
        }

        public Produkt GetProduct()
        {
            Produkt result = produkt;

            this.Reset();

            return result;
        }
    }
}
