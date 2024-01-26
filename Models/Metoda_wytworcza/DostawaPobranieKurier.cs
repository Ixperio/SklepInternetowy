using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Metoda_wytworcza
{
    //FABRYKA DOSTAW
    public class DostawaPobranieKurier : IFactoryDostawa
    {

        public List<IDostawa> createFactory()
        {
            List<IDostawa> dostawas = new List<IDostawa>();

            IDostawa kurier = new KurierPobranie("Kurier ABC Pobranie", 19.99m);
            dostawas.Add(kurier);
            IDostawa kurierDHL = new KurierPobranie("Kurier DHL Pobranie", 24.99m);
            dostawas.Add(kurierDHL);

            return dostawas;
        }

    }
}