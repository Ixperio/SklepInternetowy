using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Models.Metoda_wytworcza.Interface
{
    public interface IFactoryDostawa
    {

        public IDostawa createFactory();

    }
}
