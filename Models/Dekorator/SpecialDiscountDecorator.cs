using Sklep.Models.Dekorator.Interface;
using Sklep.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Dekorator
{
    public class SpecialDiscountDecorator : DiscountDecorator
    {
        private readonly IProducts _decorated;
        private readonly static decimal DISCOUNT_VALUE = 15; // PROCENTOWA WARTOŚĆ ZNIŻKI

        public SpecialDiscountDecorator(IProducts decorated) : base(decorated) { _decorated = decorated; }

        public override decimal getPrice()
        {
            decimal originalPrice = _decorated.getPrice();
            return Math.Ceiling((originalPrice * ((100 - DISCOUNT_VALUE) / 100)) / 0.01m) * 0.01m;

        }
        public override string decoratorName()
        {
            return "Oferta specjalna -" + DISCOUNT_VALUE + "%";
        }
    }
}