using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iText.Layout;
using iText.Layout.Element;
using Sklep.Models.Strategia.Interface;
using Sklep.Models.Dekorator.Interface;
using Sklep.Models.Dekorator;
using System.Data.Entity.Core.Metadata.Edm;
using Sklep.Models.ModelViews;
using Sklep.Models.Metoda_wytworcza.Interface;
using Sklep.Models.Metoda_wytworcza;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

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
            
            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            dokumentPdf.SetFont(font);

            Product prod = new Product(produkt.cenaNetto);

            SpecialDiscountDecorator ofSpecjalna = new SpecialDiscountDecorator(prod);
            HolidayDiscountDecorator ofWakacyjna = new HolidayDiscountDecorator(prod);
            StandardDiscountDecorator ofStandard = new StandardDiscountDecorator(prod);

            Paragraph nazwa = new Paragraph(produkt.Nazwa+" - Cena netto :"+ ofSpecjalna.getPrice() + " ,uwzglednia ("+ ofSpecjalna.decoratorName()+")");

            dokumentPdf.Add(nazwa);

            Paragraph nazwa2 = new Paragraph(produkt.Nazwa + " - Cena netto :" + ofWakacyjna.getPrice() + " ,uwzglednia (" + ofWakacyjna.decoratorName() + ")");

            dokumentPdf.Add(nazwa2);

            Paragraph nazwa3 = new Paragraph(produkt.Nazwa + " - Cena netto :" + ofStandard.getPrice() + " ,uwzglednia (" + ofStandard.decoratorName() + ")");

            dokumentPdf.Add(nazwa3);

            //DOSTAWY

            IFactoryDostawa dostawyFabrykaKurier = new DostawaKurier();
            IFactoryDostawa dostawyFabrykaKurierPobranie = new DostawaPobranieKurier();
            IDostawa dostawa = dostawyFabrykaKurier.createFactory();
            IDostawa dostawaPobranie = dostawyFabrykaKurierPobranie.createFactory();

            dokumentPdf.Add(new Paragraph("Możliwe dostawy : "));

            dokumentPdf.Add(new Paragraph(dostawa.getName() + " | " + dostawa.getPrice() + "PLN"));
            dokumentPdf.Add(new Paragraph(dostawaPobranie.getName() + " | " + dostawaPobranie.getPrice() + "PLN"));

            return dokumentPdf;

        }

    }
}