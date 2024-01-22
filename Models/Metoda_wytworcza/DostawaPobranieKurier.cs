using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Metoda_wytworcza
{
    public class DostawaPobranieKurier : IFactoryDostawa
    {

        public IDostawa createFactory()
        {
            IDostawa kurier = new KurierPobranie("Kuerier ABC Pobranie", 19.99m);
            return kurier;
        }

    }
}