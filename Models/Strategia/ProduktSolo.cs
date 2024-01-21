using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iText.Layout;
using iText.Layout.Element;
using Sklep.Models.Strategia.Interface;

namespace Sklep.Models.Strategia
{
    /**
     * @brief Klasa pozwala na wygenerowanie dokumentu pdf zawierającego informacje o produkcie.
     * 
     * @author Artur Leszczak
     */
    public class ProduktSolo : IPdfGenerator
    {
        //Tworzy plik pdf.
        public Document generatePdf(List<Produkt> products, Document dokument)
        {
            Produkt produkt = products[0];
            Document dokumentPdf = dokument;

            Paragraph nazwa = new Paragraph(produkt.Nazwa);

            dokumentPdf.Add(nazwa);

            return dokumentPdf;

        }

    }
}