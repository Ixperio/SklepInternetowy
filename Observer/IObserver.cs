using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Observer
{
    //Interfejs obserwatora - Katarzyna Grygo
    public interface IObserver
    {
        void Update(IOrderObserver order);
    }
}
