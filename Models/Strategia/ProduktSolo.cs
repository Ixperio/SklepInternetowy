﻿using System;
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
using Sklep.Db_Context;

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

        private MyDbContext _db;

        public ProduktSolo()
        {
            _db = MyDbContext.GetInstance();
        }

        public Document generatePdf(List<Produkt> products, Document dokument)
        {

            Produkt produkt = products[0];
            Document dokumentPdf = dokument;
            
            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            dokumentPdf.SetFont(font);

            dokumentPdf.Add(new Paragraph(produkt.Nazwa + " Cena netto: " + produkt.cenaNetto + "zL | Cena brutto: " + Math.Ceiling((produkt.cenaNetto * 1.23m) * 100) / 100 + "zl"));
            dokumentPdf.Add(new Paragraph(""));
            dokumentPdf.Add(new Paragraph("Opis"));
            dokumentPdf.Add(new Paragraph("--------------------------------------------------"));
            List<Section> sekcje = _db.Sekcja.Where(s => s.OpisId == 2).ToList();

            foreach(var s in sekcje)
            {
                dokumentPdf.Add(new Paragraph(""));
                dokumentPdf.Add(new Paragraph(s.Description));
            }
            dokumentPdf.Add(new Paragraph(""));
            dokumentPdf.Add(new Paragraph("Parametry techniczne"));
            dokumentPdf.Add(new Paragraph("--------------------------------------------------"));

            return dokumentPdf;

        }

    }
}