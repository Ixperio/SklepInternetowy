using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Metoda_wytworcza
{
    public class DostawaKurier : IFactoryDostawa
    {

        public List<IDostawa> createFactory()
        {
            List<IDostawa> dostawas = new List<IDostawa>();
            
            IDostawa kurier = new Kurier("Kurier ABC", 14.99m);
            dostawas.Add(kurier);
            IDostawa kurierDHL = new Kurier("Kurier DHL", 14.99m);
            dostawas.Add(kurierDHL);

            return dostawas;
        }

    }
}