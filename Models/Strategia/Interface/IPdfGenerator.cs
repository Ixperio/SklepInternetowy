using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.Models.Strategia.Interface
{
    public interface IPdfGenerator
    {
        public Document generatePdf(List<Produkt> produkts, Document document);

    }
}
