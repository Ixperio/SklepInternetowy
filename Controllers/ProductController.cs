﻿using Sklep.Models;
using Sklep.Models.Strategia.Interface;
using Sklep.Models.Strategia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.IO;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Messaging;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.IO.Font;
using System.Text;
using System.Web.Configuration;
using System.Data.Entity.Infrastructure.Interception;

namespace Sklep.Controllers
{
    public class ProductController : Controller
    {
        private MyDbContext _db;

        public ProductController()
        {
            this._db = new MyDbContext();
        }

        // GET: Product
        public ActionResult Index()
        {

            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            using(var db = _db)
            {
                    using (var transaction = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                        var product = db.Products.FirstOrDefault(p => p.ProduktId == id && p.isDeleted == false && p.isVisible == true);

                        if (product != null)
                        {
                            var rodzaj = db.Rodzaj.FirstOrDefault(r => r.Id == product.rodzajId);

                            if (rodzaj != null)
                            {
                                var kategoria = db.Kategoria.FirstOrDefault(k => k.KategoriaId == rodzaj.KategoriaId && k.isDeleted == false && k.isVisible == true);

                                if (kategoria != null)
                                {
                                    ViewBag.produkt = product;
                                    ViewBag.kategoria = kategoria;
                                    transaction.Commit();

                                    return View("Details");
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            return HttpNotFound();
                        }

                        }
                        catch(Exception)
                        {
                            return HttpNotFound();
                        }
                    }
              }

        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string name = collection["name"];
                string description = collection["description"];
                //string price = collection["price"];

                ProductBuilder builder = new ProductBuilder();
                builder.Reset();
                builder.BuildName(name);
                builder.BuildDescription(new Opis());
                //builder.BuildPrice(price.AsDecimal());

                Produkt produkt = builder.GetProduct();
                _db.Products.Add(produkt);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetProductPdf(int id)
        {
            IPdfGenerator generujPojedynczyProdukt = new ProduktSolo();

            PdfGeneartorContext context = new PdfGeneartorContext(generujPojedynczyProdukt);

            //pobierz produkt po id;

            var produkt = this.getProductById(id);

            if(produkt!=null)
            {
                List<Produkt> listaProduktow = new List<Produkt>();
                listaProduktow.Add(produkt);

                using (var memoryStream = new MemoryStream())
                {
                    var writer = new PdfWriter(memoryStream);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);

                    string fontPath = Server.MapPath("~/App_Data/Sevillana-Regular.ttf"); // Zmień YourNamespace na rzeczywistą przestrzeń nazw
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.UTF8);
                    document.SetFont(font);

                    document = context.generate(listaProduktow, document);

                    string fileName = "Produkt nr "+id+".pdf";

                    pdf.GetDocumentInfo().SetTitle(fileName);

                    document.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);

                    return File(memoryStream.ToArray(), "application/pdf", fileName);
                }

            }

            return HttpNotFound();

        }

        private Produkt? getProductById(int id) {
            var produkt = new Produkt()
            {
                ProduktId = id,
                Nazwa = _db.Products.SingleOrDefault(x => x.ProduktId == id).Nazwa,
                Ilosc_w_magazynie = _db.Products.SingleOrDefault(x => x.ProduktId == id).Ilosc_w_magazynie,
                rodzaj_miaryId = _db.Products.SingleOrDefault(x => x.ProduktId == id).rodzaj_miaryId,
                cenaNetto = _db.Products.SingleOrDefault(x => x.ProduktId == id).cenaNetto,
                vatId = _db.Products.SingleOrDefault(x => x.ProduktId == id).vatId,
                glownaWalutaId = _db.Products.SingleOrDefault(x => x.ProduktId == id).glownaWalutaId,
                rodzajId = _db.Products.SingleOrDefault(x => x.ProduktId == id).rodzajId,
                Kupiono_lacznie = _db.Products.SingleOrDefault(x => x.ProduktId == id).Kupiono_lacznie,
                adderId = _db.Products.SingleOrDefault(x => x.ProduktId == id).adderId,
                isDeleted = _db.Products.SingleOrDefault(x => x.ProduktId == id).isDeleted,
                isVisible = _db.Products.SingleOrDefault(x => x.ProduktId == id).isVisible,
                addDate = _db.Products.SingleOrDefault(x => x.ProduktId == id).addDate,
                removeDate = _db.Products.SingleOrDefault(x => x.ProduktId == id).removeDate
            };

            if (produkt.isDeleted)
            {
                return null;
            }
            if(produkt.isVisible)
            {
                return produkt;
            }
            else
            {
                return null;
            }

        }

    }
}
