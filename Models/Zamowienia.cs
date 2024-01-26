
using Sklep.Observer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Sklep.Models
{
    public class ZamowieniaKlienci : IOrderObserver
    {
        public int Id { get; set; }
        public int zamawiajacyId { get; set; }

        public int adresId { get; set; }
        public int dostawaId { get; set; }
        public int platnosc_typId { get; set; }
        public int walutaId { get; set; }

        public DateTime addDate { get; set; }

        public string status { get; private set; }
        public decimal kwota { get; set; }

        public List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        public void Notify()
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void SetStatus(string s)
        {
            status = s;
            Notify();
        }
    }
}
