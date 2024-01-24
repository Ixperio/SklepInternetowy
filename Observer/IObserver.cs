using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Observer
{
    public interface IObserver
    {
        void Update(IOrderObserver order);
    }
}
