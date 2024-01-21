using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iText.Layout;
using iText.Layout.Element;
using Sklep.Models.Strategia.Interface;

namespace Sklep.Models.Strategia
{
    public class PdfGeneartorContext
    {
        private readonly IPdfGenerator pdfGenerator;

        public PdfGeneartorContext(IPdfGenerator pdfGenerator)
        {
            this.pdfGenerator = pdfGenerator;
        }

        public Document generate(List<Produkt> products, Document doc)
        {
            return pdfGenerator.generatePdf(products,doc);
        }

    }
}