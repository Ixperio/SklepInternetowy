using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Memento
{
    public class Memento
    {
        public string State { get; }

        public Memento(string state)
        {
            State = state;
        }
    }
}