using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Dekorator
{
    public class HolidayDiscountDecorator : DiscountDecorator
    {
        private float discount_procentage;

        public HolidayDiscountDecorator()
        {

            this.discount_procentage = 5;

        }

        public float getDiscount()
        {
            return this.discount_procentage;
        }
    }
}