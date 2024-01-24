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
using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.Strategia.Interface;
using Sklep.Models.Strategia;
using Sklep.Models.ModelViews;
using Sklep.Prototype;

namespace Sklep.Controllers
{
    public class ProductController : Controller
    {
        private MyDbContext _db;

        public ProductController()
        {
            this._db = MyDbContext.GetInstance();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                //builder.BuildDescription(new Opis());
                //builder.BuildPrice(price.AsDecimal());

                Produkt produkt = builder.GetProduct();
                MyDbContext _db = MyDbContext.GetInstance();
                _db.Products.Add(produkt);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
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

                    document = context.generate(listaProduktow, document);

                    document.Close();
                    Response.Headers.Add("Content-Disposition", "inline; filename=example.pdf");
                    return File(memoryStream.ToArray(), "application/pdf");
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

        public ActionResult Comments(int id)
        {
            var produkt = getProductById(id);

            if (produkt != null)
            {
                return View(produkt.Komentarze.ToList());
            }

            return HttpNotFound();
        }

        //https://localhost:44386/Product/AddComment?productId=1
        [HttpGet]
        public ActionResult AddComment(int productId)
        {
            var produkt = _db.Products.Find(productId);
            if (produkt == null)
            {
                return HttpNotFound();
            }

            var productComment = new ProductComment { ProduktId = productId };
            return View(productComment);
        }

        [HttpPost]
        public ActionResult AddComment(ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                Comment prototypeComment = new Comment { UserName = productComment.UserName, Content = productComment.Content };
                Comment newComment = prototypeComment.DeepCopy();

                var produkt = _db.Products.Find(productComment.ProduktId);
                if (produkt != null)
                {
                    var komentarz = new Komentarz
                    {
                        UserName = newComment.UserName,
                        Content = newComment.Content,
                        CreatedAt = DateTime.Now
                    };
                    _db.Komentarze.Add(komentarz);
                    produkt.Komentarze.Add(komentarz);
                    _db.SaveChanges();

                    ViewBag.Message = "Comment added successfully!";
                }
            }

            return View(productComment);
        }

    }
}
