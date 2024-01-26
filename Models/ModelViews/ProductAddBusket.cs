using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.ModelViews
{
    //Klasa potrzebna do wykorzystania w cookies w celu przechowywania modelu informacji
    //o produktach znajdujących się w koszyku - Artur Leszczak
    public class ProductAddBusket
    {
        public int ProduktId { get; set; }
        public int Liczba {  get; set; }
    }
}