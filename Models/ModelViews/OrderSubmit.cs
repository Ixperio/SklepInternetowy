using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Models.ModelViews
{
    public enum PaymentMethod{
        Blik,
        Credit_Card,
        Cash
    };

    public class OrderSubmit
    {
        public string Street {  get; set; }
        public string Number { get; set; }
        public string Flat_Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public PaymentMethod paymentMethod { get; set; }
    }
}
