using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep.Models.Dekorator
{
    public class SpecialDiscountDecorator : DiscountDecorator
    {
        private float discount_procentage;

        public SpecialDiscountDecorator() { 
            
            this.discount_procentage = 15;
        
        }

        public float getDiscount()
        {
            return this.discount_procentage;
        }

    }
}