using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Metoda_wytworcza
{
    public class DostawaKurier : IFactoryDostawa
    {

        public IDostawa createFactory()
        {
            IDostawa kurier = new Kurier("Kurier ABC", 14.99m);
            return kurier;
        }

    }
}