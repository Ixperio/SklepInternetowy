using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Dekorator
{
    public class StandardDiscountDecorator : DiscountDecorator
    {
        private float discount_procentage;

        public StandardDiscountDecorator()
        {

            this.discount_procentage = 5;

        }

        public float getDiscount()
        {
            return this.discount_procentage;
        }
    }
}