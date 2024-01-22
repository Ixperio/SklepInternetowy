using Sklep.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sklep.Models.Interfaces;
using Sklep.Models.Dekorator.Interface;


namespace Sklep.Models.Dekorator
{
    public abstract class DiscountDecorator : IProducts
    {
        private readonly IProducts _decoratedProducts; 
        public DiscountDecorator(IProducts decoratedProduct) { 
            _decoratedProducts = decoratedProduct;
        }

        public abstract decimal getPrice();

        public abstract string decoratorName();

    }
}