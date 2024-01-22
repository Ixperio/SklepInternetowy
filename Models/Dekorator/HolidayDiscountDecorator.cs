using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sklep.Models.Dekorator.Interface;
using Sklep.Models.Interfaces;

namespace Sklep.Models.Dekorator
{
    /**
     * @brief klasa dekorująca cenę produktu na określoną zniżkę
     * @author Artur Leszczak
     */
    public class HolidayDiscountDecorator : DiscountDecorator
    {
        private readonly IProducts _decorated;
        private readonly static decimal DISCOUNT_VALUE = 10; // PROCENTOWA WARTOŚĆ ZNIŻKI

        public HolidayDiscountDecorator(IProducts decorated) : base(decorated){ _decorated = decorated; }

        public override decimal getPrice()
        {
            decimal originalPrice = _decorated.getPrice();
            return Math.Ceiling((originalPrice * ((100 - DISCOUNT_VALUE) / 100)) / 0.01m) * 0.01m;

        }

        public override string decoratorName()
        {
            return "Oferta wakacyjna -" + DISCOUNT_VALUE + "%";
        }

    }
}