using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Font;
using iText.IO.Font;
using Sklep.Models.ModelViews;
using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.Strategia.Interface;
using Sklep.Models.Strategia;
using Sklep.Prototype;
using Microsoft.Extensions.Logging.Abstractions;
using System.Runtime.Remoting;
using Microsoft.Ajax.Utilities;

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
        //pobieranie wszystkich produktów - Artur Leszczak
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
                            decimal podatek = _db.Podatek.FirstOrDefault(pod => pod.Id == p.vatId).stawka;
                            if (podatek == null)
                            {
                                podatek = 23m;
                            }
                            decimal cenaBrutto = p.cenaNetto*(1+(podatek)/100);
                            cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;

                            string kategoriaNazwa = db.Kategoria.FirstOrDefault(k =>
                            k.KategoriaId == db.Rodzaj.FirstOrDefault(r => r.Id == p.rodzajId).KategoriaId && k.isDeleted == false && k.isVisible == true).Name;

                            string imageUrl = db.Photo.FirstOrDefault(d => d.ProductId == p.ProduktId && d.SectionId == 0).link;

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
        //pobieranie detali produktu po Id - Artur Leszczak
        [HttpGet]
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

                                    decimal podatek = _db.Podatek.FirstOrDefault(pod => pod.Id == product.vatId).stawka;
                                    if (podatek == null)
                                    {
                                        podatek = 23m;
                                    }
                                    decimal cenaBrutto = product.cenaNetto * (1 + (podatek)/100);
                                    cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;        

                                    string imageUrl = db.Photo.FirstOrDefault(d => d.ProductId == product.ProduktId && d.SectionId == 0).link;

                                    if (string.IsNullOrEmpty(imageUrl))
                                    {
                                        imageUrl = "/Images/NoIcon.PNG";
                                    }

                                    if (!string.IsNullOrEmpty(kategoria.Name))
                                    {
                                        //tworzy model widoku dla podstawowych parametrów
                                        var prod = new ProductListAll()
                                        {
                                            Id = product.ProduktId,
                                            Name = product.Nazwa,
                                            StoreCount = product.Ilosc_w_magazynie,
                                            BruttoPrice = cenaBrutto,
                                            NettoPrice = product.cenaNetto,
                                            CategoryName = kategoria.Name,
                                            OpinionCounter = 10,
                                            ImageUrl = imageUrl,
                                            OpinionValue = 4.8m,
                                        };

                                        ViewBag.produkt = prod;
                                        ViewBag.kategoria = kategoria;
                                        ViewBag.opis = this.GetOpis(id);
                                        ViewBag.parametry = this.GetParametryWidok(id);
                                        ViewBag.opinie = this.GetComments(id);

                                        return View("Details");

                                    }
                                    return HttpNotFound();

                            }
                            }
                  }
                  else
                  {
                        System.Diagnostics.Debug.WriteLine($"Problem 1");
                        return HttpNotFound();
                  }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Problem 2 - "+ex.Message);
                    return HttpNotFound();
                }
            }
            System.Diagnostics.Debug.WriteLine($"Problem 3");
            return HttpNotFound();
        }
        /**@brief Metoda zwraca paramtery wchodzące w skład informacji produktowej
         * 
         * @author Artur Leszczak
         */
        private List<ParametryWidok> GetParametryWidok(int id)
        {
                var parametry_W_produkcie = _db.Parametr_w_produkcie.Where(p => p.ProduktId == id && p.isVisible == true && p.isDeleted == false).ToList();

                if (parametry_W_produkcie.Count > 0)
                {
                    List<ParametryWidok> pw = new List<ParametryWidok>();

                    foreach (var pwp in parametry_W_produkcie)
                    {
                        var parametr = _db.Parametr.FirstOrDefault(p => p.Parametrid == pwp.ParametrId);
                        if (parametr != null)
                        {
                            ParametryWidok parametryWidok = new ParametryWidok()
                            {
                                Nazwa = parametr.name,
                                Wartosc = pwp.Value + "" + parametr.jednostka
                            };
                            pw.Add(parametryWidok);
                        }
                    }
                    return pw;
                }
                else
                {
                    return null;
                }
            
        }

        private List<OpisWidok> GetOpis(int id)
        {
            var opis = _db.Description.FirstOrDefault(o => o.ProduktId == id && o.isVisible == true && o.isDeleted == false);

            var sections = _db.Sekcja.Where(p => p.OpisId == opis.OpisId && p.isVisible == true && p.isDeleted == false).ToList();

            if (sections.Count > 0)
            {
                List<OpisWidok> pw = new List<OpisWidok>();

                foreach (var sec in sections)
                {
                    OpisWidok ow = new OpisWidok()
                    {
                        Opis = sec.Description
                    };

                    pw.Add(ow);
                }
                return pw;
            }
            else
            {
                return null;
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
        //pobieranie produktu po Id - Artur Leszczak
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
        //pobieranie produktu po Id - Artur Leszczak
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

        //pobieranie produktu po Id - Artur Leszczak
        private Produkt getProductById(int id) {

            var produkt = new Produkt();

            produkt = _db.Products.SingleOrDefault(x => x.ProduktId == id);
            if(produkt != null)
            {
                if (produkt.isDeleted)
                {
                    return null;
                }
                if (produkt.isVisible)
                {
                    return produkt;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        //pobranie komentarzy - Katarzyna Grygo
        private List<ProductComment> GetComments(int id)
        {
            var produkt = getProductById(id);

            if (produkt != null)
            {

                List<Komentarz> komentarze = new List<Komentarz>();

                komentarze = _db.Komentarze.Where(c=>c.ProduktId == id).ToList();

                List<ProductComment> komentarz_produktu = new List<ProductComment>();
                
                foreach (var comment in komentarze)
                {
                    var p = _db.Person.SingleOrDefault(p => p.PersonId == comment.UserId);
                    if (p != null)
                    {
                        ProductComment productComment = new ProductComment()
                        {
                            ProduktId = comment.ProduktId,
                            UserName = p.Name+" "+p.Surname,
                            Content = comment.Content
                        };
                        komentarz_produktu.Add(productComment);
                    }
                    else
                    {
                        ProductComment productComment = new ProductComment()
                        {
                            ProduktId = comment.ProduktId,
                            UserName = "<i>Użytkownik Nieznany</i>",
                            Content = comment.Content
                        };
                        komentarz_produktu.Add(productComment);
                    }
                }

                return komentarz_produktu;
            }
            return null;
        }

        //dodawanie komentarzy - Katarzyna Grygo
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
        //dodawanie komentarzy - Katarzyna Grygo
        [HttpPost]
        public ActionResult AddComment(ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                CommentPrototype prototypeComment = new CommentPrototype { UserName = productComment.UserName, Content = productComment.Content };
                CommentPrototype newComment = prototypeComment.DeepCopy();

                var produkt = _db.Products.Find(productComment.ProduktId);
                if (produkt != null)
                {
                    var komentarz = new Komentarz
                    {
                        UserId = 1,
                        Content = newComment.Content,
                        CreatedAt = DateTime.Now
                    };
                    _db.Komentarze.Add(komentarz);
                    _db.SaveChanges();

                    ViewBag.Message = "Comment added successfully!";
                }
            }

            return View(productComment);
        }

    }
}
