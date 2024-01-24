using Sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Observer
{
    public class EmailNotification : IObserver
    {
        public void Update(IOrderObserver order)
        {
            Console.WriteLine((order as Zamowienia).status); //zmienic na maila
        }
    }
}
