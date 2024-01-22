using Sklep.Models.Dekorator.Interface;
using Sklep.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Dekorator
{
    public class StandardDiscountDecorator : DiscountDecorator
    {
        private readonly IProducts _decorated;
        private readonly static decimal DISCOUNT_VALUE = 5; // PROCENTOWA WARTOŚĆ ZNIŻKI

        public StandardDiscountDecorator(IProducts decorated) : base(decorated) { _decorated = decorated; }

        public override decimal getPrice()
        {
            decimal originalPrice = _decorated.getPrice();
            return Math.Ceiling((originalPrice * ((100 - DISCOUNT_VALUE) / 100))/0.01m)*0.01m;

        }

        public override string decoratorName()
        {
            return "Przecena -" + DISCOUNT_VALUE + "%";
        }

    }
}