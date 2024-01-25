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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Sklep.Models.ModelViews;
using System.Data.Entity;

using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.Strategia.Interface;
using Sklep.Models.Strategia;
using Sklep.Models.ModelViews;
using Sklep.Prototype;
using iText.Commons.Actions.Producer;


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
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        public ActionResult Products(Pages page)
        {
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            using (var db = _db)
            {
                try
                {
                    List<Produkt> products = db.Products.Where(p => p.isDeleted == false && p.isVisible == true).ToList();

                    if (products != null)
                    {
                        List<ProductListAll> produkty = new List<ProductListAll>();
                        foreach (Produkt p in products)
                        {

                            decimal cenaBrutto = p.cenaNetto*1.23m;
                            cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;

                            string kategoriaNazwa = db.Kategoria.FirstOrDefault(k =>
                            k.KategoriaId == db.Rodzaj.FirstOrDefault(r => r.Id == p.rodzajId).KategoriaId && k.isDeleted == false && k.isVisible == true).Name;

                            string imageUrl = db.Photo.FirstOrDefault(p => p.ProductId == p.ProductId && p.SectionId == 0).link;

                            if (string.IsNullOrEmpty(imageUrl))
                            {
                                imageUrl = "/Images/NoIcon.PNG";
                            }

                            if (!string.IsNullOrEmpty(kategoriaNazwa))
                            {
                                var prod = new ProductListAll()
                                {
                                    Id = p.ProduktId,
                                    Name = p.Nazwa,
                                    StoreCount = p.Ilosc_w_magazynie,
                                    BruttoPrice = cenaBrutto,
                                    NettoPrice = p.cenaNetto,
                                    CategoryName = kategoriaNazwa,
                                    OpinionCounter = 10,
                                    ImageUrl = imageUrl,
                                    OpinionValue = 4.8m,
                                };

                                produkty.Add(prod);
                            }

                        }

                        ViewBag.Products = produkty;
                        return View("Products");
                    }
                    else
                    {

                        return HttpNotFound();
                    }

                }
                catch (Exception)
                {
                    return HttpNotFound();
                }
            }

        }

        //GET: Product/Details/5
        public ActionResult Details(int id)
        {
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            using (var db = _db)
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
                                    var opinie = db.Comment.Where(c => c.ProductId == product.ProduktId).ToList();

                                    
                                    ViewBag.opinie = opinie;

                                    return View("Details");
                                }
                            }
                        }
                        else
                        {
                       
                            return HttpNotFound();
                        }

                }
                catch (Exception)
                {
                    return HttpNotFound();
                }
            }

        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];
                var user = _db.Person.Find(userId);

                if (user != null && user.AccountTypeId == 2) 
                {
                    return View();
                }
            }

            return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult AddProduct(FormCollection collection)
        {
            try
            {
                string name = collection["Nazwa"];
                string amount = collection["Ilosc_w_magazynie"];
                string price = collection["cenaNetto"];

                ProductBuilder builder = new ProductBuilder();
                builder.Reset();
                builder.BuildName(name);
                builder.BuildAmount(Convert.ToInt32(amount));
                builder.BuildPrice(Convert.ToDecimal(price));

                Produkt produkt = builder.GetProduct();
                MyDbContext _db = MyDbContext.GetInstance();
                _db.Products.Add(produkt);
                _db.SaveChanges();

                return View();
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
            //WYBÓR STRATEGII
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

                    string fontPath = Server.MapPath("~/App_Data/Sevillana-Regular.ttf"); 
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.UTF8);
                    document.SetFont(font);

                    //UŻYCIE STRATEGIA
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

        [HttpGet]
        public ActionResult GetProductsPdf()
        {
            //WYBÓR STRATEGII
            IPdfGenerator generujKatalog = new KatalogProduktow();
           
            PdfGeneartorContext context = new PdfGeneartorContext(generujKatalog);

                List<Produkt> listaProduktow = new List<Produkt>();
                
                listaProduktow = _db.Products.Where(p => p.isVisible == true && p.isDeleted == false).ToList();

                using (var memoryStream = new MemoryStream())
                {
                    var writer = new PdfWriter(memoryStream);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);

                    string fontPath = Server.MapPath("~/App_Data/Sevillana-Regular.ttf");
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.UTF8);
                    document.SetFont(font);
                    
                    //UŻYCIE STRATEGIA
                    document = context.generate(listaProduktow, document);

                    string fileName = "Katalog produktów skelpu.pdf";

                    pdf.GetDocumentInfo().SetTitle(fileName);

                    document.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline; filename=" + fileName);

                    return File(memoryStream.ToArray(), "application/pdf", fileName);
                }

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
