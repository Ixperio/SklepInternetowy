using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using Sklep.Models.ModelViews;
using Sklep.Models.Strategia.Interface;

namespace Sklep.Models.Strategia
{
    public class KatalogProduktow : IPdfGenerator
    {
        private static MyDbContext _db;
        public KatalogProduktow()
        {
            _db = new MyDbContext();
        }
        public Document generatePdf(List<Produkt> list, Document dokument)
        {

            Document dokumentPdf = dokument;

            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            dokumentPdf.SetFont(font);
            dokumentPdf.Add(new Paragraph("Nr produktu   | Nazwa kategorii | Nazwa produktu           | Cena netto       | Cena brutto      |"));
            dokumentPdf.Add(new Paragraph("---------------------------------------------------------------------------------------------------------------------------"));
            foreach (Produkt produkt in list)
            {
                int kategoriaId = _db.Rodzaj.FirstOrDefault(r => r.Id == produkt.rodzajId).KategoriaId;
                string kategoriaName = null;
                if (kategoriaId != null)
                {
                    kategoriaName = _db.Kategoria.FirstOrDefault(k => k.KategoriaId == kategoriaId).Name;
                }
                if (!string.IsNullOrEmpty(kategoriaName))
                {
                    dokumentPdf.Add(new Paragraph("Nr produktu : " + produkt.ProduktId + " | "+kategoriaName+" | " + produkt.Nazwa + " | Cena netto: " + produkt.cenaNetto + "zl | Cena brutto: " + Math.Ceiling((produkt.cenaNetto * 1.23m) * 100) / 100 + "zl"));
                    dokumentPdf.Add(new Paragraph("---------------------------------------------------------------------------------------------------------------------------"));
                }
                
            }

            return dokumentPdf;

        }
    }
}