using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iText.Layout;
using iText.Layout.Element;
using Sklep.Models.Strategia.Interface;

namespace Sklep.Models.Strategia
{
    public class KatalogProduktow : IPdfGenerator
    {
        public Document generatePdf(List<Produkt> list, Document dokument)
        {

            Document dokumentPdf = dokument;




            return dokumentPdf;

        }
    }
}