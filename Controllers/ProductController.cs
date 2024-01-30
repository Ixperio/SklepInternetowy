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
using Sklep.Models.Dekorator;
using Sklep.Models.Interfaces;
using iText.Layout.Borders;

namespace Sklep.Controllers
{
    public class ProductController : Controller
    {
        private MyDbContext _db;

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
            using (var _db = new MyDbContext())
            {
                try
                {
                    List<Produkt> products = _db.Products.Where(p => p.isDeleted == false && p.isVisible == true).ToList();

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

                            //SPRAWDŹ CZY PRODUKT JEST OBJĘTY PROMOCJĄ

                            var promocja = _db.Promocja_produkt.FirstOrDefault(c => c.ProduktId == p.ProduktId && c.isDeleted == false);
                            decimal cenaBrutto = 0m;
                            decimal cenaNetto = p.cenaNetto;
                            string nazwaPromocji = "";
                            if (promocja != null)
                            {
                                    //utwórz odpowiedni dekorator ceny
                                    
                                    Product nwProd = new Product(cenaNetto);
                                    System.Diagnostics.Debug.WriteLine($"Promocja");
                                    switch (promocja.PromocjaId)
                                    {
                                        case 1:
                                            System.Diagnostics.Debug.WriteLine($"Oferta standardowa");
                                            StandardDiscountDecorator standard = new StandardDiscountDecorator(nwProd);
                                            cenaBrutto = standard.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = standard.getPrice();
                                            nazwaPromocji = standard.decoratorName();
                                            break;
                                        case 2:
                                            System.Diagnostics.Debug.WriteLine($"Oferta wakacyjna");
                                            HolidayDiscountDecorator holiday = new HolidayDiscountDecorator(nwProd);
                                            cenaBrutto = holiday.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = holiday.getPrice();
                                            nazwaPromocji = holiday.decoratorName();
                                            break;
                                        case 3:
                                            System.Diagnostics.Debug.WriteLine($"Oferta specjalna");
                                            SpecialDiscountDecorator special = new SpecialDiscountDecorator(nwProd);
                                            cenaBrutto = special.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = special.getPrice();
                                            nazwaPromocji = special.decoratorName();
                                            break;
                                        default:
                                            System.Diagnostics.Debug.WriteLine($"Brak");
                                            cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                            break;
                                    }
                                    if(cenaBrutto<= 0m)
                                    {
                                        cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }
                                    else
                                    {
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }
                                    
                                }
                                else
                                {
                                    cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                    cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                }

                            decimal cenaBruttoOld = p.cenaNetto * (1 + (podatek) / 100);
                            cenaBruttoOld = Math.Ceiling(cenaBruttoOld * 100) / 100;

                            string kategoriaNazwa = _db.Kategoria.FirstOrDefault(k =>
                            k.KategoriaId == _db.Rodzaj.FirstOrDefault(r => r.Id == p.rodzajId).KategoriaId &&
                            k.isDeleted == false && k.isVisible == true).Name;

                            string imageUrl = _db.Photo.FirstOrDefault(d => d.ProductId == p.ProduktId && d.SectionId == 0).link;

                            if (string.IsNullOrEmpty(imageUrl))
                            {
                                imageUrl = "/Images/NoIcon.PNG";
                            }

                            if (!string.IsNullOrEmpty(kategoriaNazwa))
                            {

                                if (string.IsNullOrEmpty(nazwaPromocji))
                                {
                                    nazwaPromocji = null;
                                }

                                var prod = new ProductListAll()
                                {
                                    Id = p.ProduktId,
                                    Name = p.Nazwa,
                                    StoreCount = p.Ilosc_w_magazynie,
                                    BruttoPrice = cenaBrutto,
                                    BruttoPriceOld = cenaBruttoOld,
                                    NettoPrice = cenaNetto,
                                    NettoPriceOld = p.cenaNetto,
                                    CategoryName = kategoriaNazwa,
                                    OpinionCounter = 10,
                                    ImageUrl = imageUrl,
                                    OpinionValue = 4.8m,
                                    DiscountName = nazwaPromocji,
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


        public ActionResult Category(int id)
        {
            using (var _db = new MyDbContext())
            {
                var rodzaj = _db.Rodzaj.Where(r => r.KategoriaId == id).Select(r => r.Id).ToList();

                if (rodzaj.Count() > 0)
                {
                  
                    try
                    {
                        List<Produkt> products = _db.Products.Where(p => p.isDeleted == false && p.isVisible == true && rodzaj.Contains(p.rodzajId)).ToList();
                      
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

                                //SPRAWDŹ CZY PRODUKT JEST OBJĘTY PROMOCJĄ

                                var promocja = _db.Promocja_produkt.FirstOrDefault(c => c.ProduktId == p.ProduktId && c.isDeleted == false);
                                decimal cenaBrutto = 0m;
                                decimal cenaNetto = p.cenaNetto;
                                string nazwaPromocji = "";
                                if (promocja != null)
                                {
                                    //utwórz odpowiedni dekorator ceny

                                    Product nwProd = new Product(cenaNetto);
                                    System.Diagnostics.Debug.WriteLine($"Promocja");
                                    switch (promocja.PromocjaId)
                                    {
                                        case 1:
                                            System.Diagnostics.Debug.WriteLine($"Oferta standardowa");
                                            StandardDiscountDecorator standard = new StandardDiscountDecorator(nwProd);
                                            cenaBrutto = standard.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = standard.getPrice();
                                            nazwaPromocji = standard.decoratorName();
                                            break;
                                        case 2:
                                            System.Diagnostics.Debug.WriteLine($"Oferta wakacyjna");
                                            HolidayDiscountDecorator holiday = new HolidayDiscountDecorator(nwProd);
                                            cenaBrutto = holiday.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = holiday.getPrice();
                                            nazwaPromocji = holiday.decoratorName();
                                            break;
                                        case 3:
                                            System.Diagnostics.Debug.WriteLine($"Oferta specjalna");
                                            SpecialDiscountDecorator special = new SpecialDiscountDecorator(nwProd);
                                            cenaBrutto = special.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = special.getPrice();
                                            nazwaPromocji = special.decoratorName();
                                            break;
                                        default:
                                            System.Diagnostics.Debug.WriteLine($"Brak");
                                            cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                            break;
                                    }
                                    if (cenaBrutto <= 0m)
                                    {
                                        cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }
                                    else
                                    {
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }

                                }
                                else
                                {
                                    cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                    cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                }

                                decimal cenaBruttoOld = p.cenaNetto * (1 + (podatek) / 100);
                                cenaBruttoOld = Math.Ceiling(cenaBruttoOld * 100) / 100;

                                string kategoriaNazwa = _db.Kategoria.FirstOrDefault(k =>
                                k.KategoriaId == _db.Rodzaj.FirstOrDefault(r => r.Id == p.rodzajId).KategoriaId && k.isDeleted == false && k.isVisible == true).Name;

                                string imageUrl = _db.Photo.FirstOrDefault(d => d.ProductId == p.ProduktId && d.SectionId == 0).link;

                                if (string.IsNullOrEmpty(imageUrl))
                                {
                                    imageUrl = "/Images/NoIcon.PNG";
                                }

                                if (!string.IsNullOrEmpty(kategoriaNazwa))
                                {

                                    if (string.IsNullOrEmpty(nazwaPromocji))
                                    {
                                        nazwaPromocji = null;
                                    }

                                    var prod = new ProductListAll()
                                    {
                                        Id = p.ProduktId,
                                        Name = p.Nazwa,
                                        StoreCount = p.Ilosc_w_magazynie,
                                        BruttoPrice = cenaBrutto,
                                        BruttoPriceOld = cenaBruttoOld,
                                        NettoPrice = cenaNetto,
                                        NettoPriceOld = p.cenaNetto,
                                        CategoryName = kategoriaNazwa,
                                        OpinionCounter = 10,
                                        ImageUrl = imageUrl,
                                        OpinionValue = 4.8m,
                                        DiscountName = nazwaPromocji,
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
            return HttpNotFound();
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
            using (var _db = new MyDbContext())
            {
                try
                {
                    var product = _db.Products.FirstOrDefault(p => p.ProduktId == id && p.isDeleted == false && p.isVisible == true);

                    if (product != null)
                    {
                        var rodzaj = _db.Rodzaj.FirstOrDefault(r => r.Id == product.rodzajId);

                        if (rodzaj != null)
                        {
                            var kategoria = _db.Kategoria.FirstOrDefault(k => k.KategoriaId == rodzaj.KategoriaId && k.isDeleted == false && k.isVisible == true);

                                if (kategoria != null)
                                {

                                decimal podatek = _db.Podatek.FirstOrDefault(pod => pod.Id == product.vatId).stawka;
                                if (podatek == null)
                                {
                                    podatek = 23m;
                                }

                                //SPRAWDŹ CZY PRODUKT JEST OBJĘTY PROMOCJĄ

                                var promocja = _db.Promocja_produkt.FirstOrDefault(c => c.ProduktId == product.ProduktId && c.isDeleted == false);
                                decimal cenaBrutto = 0m;
                                decimal cenaNetto = product.cenaNetto;
                                string nazwaPromocji = "";
                                if (promocja != null)
                                {
                                    //utwórz odpowiedni dekorator ceny

                                    Product nwProd = new Product(cenaNetto);
                                    System.Diagnostics.Debug.WriteLine($"Promocja");
                                    switch (promocja.PromocjaId)
                                    {
                                        case 1:
                                            System.Diagnostics.Debug.WriteLine($"Oferta standardowa");
                                            StandardDiscountDecorator standard = new StandardDiscountDecorator(nwProd);
                                            cenaBrutto = standard.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = standard.getPrice();
                                            nazwaPromocji = standard.decoratorName();
                                            break;
                                        case 2:
                                            System.Diagnostics.Debug.WriteLine($"Oferta wakacyjna");
                                            HolidayDiscountDecorator holiday = new HolidayDiscountDecorator(nwProd);
                                            cenaBrutto = holiday.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = holiday.getPrice();
                                            nazwaPromocji = holiday.decoratorName();
                                            break;
                                        case 3:
                                            System.Diagnostics.Debug.WriteLine($"Oferta specjalna");
                                            SpecialDiscountDecorator special = new SpecialDiscountDecorator(nwProd);
                                            cenaBrutto = special.getPrice() * (1 + (podatek) / 100);
                                            cenaNetto = special.getPrice();
                                            nazwaPromocji = special.decoratorName();
                                            break;
                                        default:
                                            System.Diagnostics.Debug.WriteLine($"Brak");
                                            cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                            break;
                                    }
                                    if (cenaBrutto <= 0m)
                                    {
                                        cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }
                                    else
                                    {
                                        cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                    }
                                    System.Diagnostics.Debug.WriteLine($"Sprawdzenie 1");
                                }
                                else
                                {
                                    cenaBrutto = cenaNetto * (1 + (podatek) / 100);
                                    cenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100;
                                }
                                System.Diagnostics.Debug.WriteLine($"Sprawdzenie 2");
                                decimal cenaBruttoOld = product.cenaNetto * (1 + (podatek) / 100);
                                cenaBruttoOld = Math.Ceiling(cenaBruttoOld * 100) / 100;
                                System.Diagnostics.Debug.WriteLine($"Sprawdzenie 3");
                                string imageUrl = _db.Photo.FirstOrDefault(d => d.ProductId == product.ProduktId && d.SectionId == 0).link;
                                System.Diagnostics.Debug.WriteLine($"Sprawdzenie 4");

                                if (string.IsNullOrEmpty(imageUrl))
                                    {
                                        imageUrl = "/Images/NoIcon.PNG";
                                    }

                                    if (!string.IsNullOrEmpty(kategoria.Name))
                                    {
                                    System.Diagnostics.Debug.WriteLine($"Sprawdzenie 5");
                                    //tworzy model widoku dla podstawowych parametrów
                                    var prod = new ProductListAll()
                                        {
                                            Id = product.ProduktId,
                                            Name = product.Nazwa,
                                            StoreCount = product.Ilosc_w_magazynie,
                                            BruttoPrice = cenaBrutto,
                                            BruttoPriceOld = cenaBruttoOld,
                                            NettoPrice = cenaNetto,
                                            NettoPriceOld = product.cenaNetto,
                                            CategoryName = kategoria.Name,
                                            OpinionCounter = 10,
                                            ImageUrl = imageUrl,
                                            OpinionValue = 4.8m,
                                            DiscountName = nazwaPromocji
                                        };

                                        ViewBag.produkt = prod;
                                        ViewBag.kategoria = kategoria;
                                        ViewBag.opis = this.GetOpis(id);
                                        ViewBag.parametry = this.GetParametryWidok(id);
                                        ViewBag.opinie = this.GetComments(id);

                                        return View("Details");

                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"Sprawdzenie 6");
                                    return HttpNotFound();

                                }
                                   

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
            using(var _db = new MyDbContext())
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
                
            
        }

        private List<OpisWidok> GetOpis(int id)
        {
            using (var _db = new MyDbContext())
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
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["UserId"] != null)
            {
                using (var _db = new MyDbContext())
                {
                    int userId = (int)Session["UserId"];
                    var user = _db.Person.Find(userId);

                    if (user != null && user.AccountTypeId == 2)
                    {
                        return View();
                    }
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

                produkt.isVisible = true;
                produkt.vatId = 1;
                produkt.rodzajId = 1;
                produkt.rodzaj_miaryId = 1;
                produkt.adderId = 1;
                produkt.glownaWalutaId = 1;

                _db.Products.Add(produkt);
                _db.SaveChanges();

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CheckProducts()
        {
            if (Session["UserId"] != null)
            {
                using (var _db = new MyDbContext())
                {
                    int userId = (int)Session["UserId"];
                    var user = _db.Person.Find(userId);

                    if (user != null && user.AccountTypeId == 2)
                    {
                        List<Produkt> products = _db.Products.ToList();

                        return View("~/Views/Product/CheckProducts.cshtml", products);
                    }
                }
            }
            return RedirectToAction("Login", "Person");
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
            using (var _db = new MyDbContext())
            {
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

        }

        //pobieranie produktu po Id - Artur Leszczak
        private Produkt getProductById(int id) {

            var produkt = new Produkt();
            using (var _db = new MyDbContext())
            {
                produkt = _db.Products.SingleOrDefault(x => x.ProduktId == id);
                if (produkt != null)
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
            }

            return null;
        }
        //pobranie komentarzy - Katarzyna Grygo
        private List<CommentPrototype> GetComments(int id)
        {
            var produkt = getProductById(id);

            if (produkt != null)
            {

                List<Komentarz> komentarze = new List<Komentarz>();
                using (var _db = new MyDbContext())
                {
                    komentarze = _db.Komentarze.Where(c => c.ProduktId == id).ToList();

                    //ZASTOSOWANIE PROTOTYPU

                    List<CommentPrototype> komentarz_produktu = new List<CommentPrototype>();

                    CommentPrototype orginalComment = new CommentPrototype
                    {
                        UserName = "",
                        Content = ""
                    };

                    foreach (var comment in komentarze)
                    {
                        var p = _db.Person.SingleOrDefault(p => p.PersonId == comment.UserId);
                        if (p != null)
                        {

                            CommentPrototype copy = orginalComment.ShallowCopy();
                            copy.UserName = p.Name;
                            copy.Content = comment.Content;

                            komentarz_produktu.Add(copy);
                        }
                        else
                        {
                            CommentPrototype copy = orginalComment.DeepCopy();
                            copy.UserName = "<i>Użytkownik nieznany</i>";
                            copy.Content = comment.Content;

                            komentarz_produktu.Add(copy);
                        }
                    }
                    return komentarz_produktu;
                }
            }
            return null;
        }

        //dodawanie komentarzy - Katarzyna Grygo
        [HttpGet]
        public ActionResult AddComment(int productId)
        {
            using (var _db = new MyDbContext())
            {
                var produkt = _db.Products.Find(productId);
                if (produkt == null)
                {
                    return HttpNotFound();
                }

                var productComment = new ProductComment { ProduktId = productId };
                return View(productComment);
            }
        }
        //dodawanie komentarzy - Katarzyna Grygo
        [HttpPost]
        public ActionResult AddComment(ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                CommentPrototype prototypeComment = new CommentPrototype { UserName = productComment.UserName, Content = productComment.Content };
                CommentPrototype newComment = prototypeComment.DeepCopy();
                using (var _db = new MyDbContext())
                {
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
            }

            return View(productComment);
        }

    }
}
