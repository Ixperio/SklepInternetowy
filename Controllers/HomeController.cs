using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.Dekorator;
using Sklep.Models.Interfaces;
using Sklep.Models.ModelViews;
using Sklep.Models.Utils;
using Sklep.Observer; //zawiera kontekst globalny

namespace Sklep.Controllers
{


    public class HomeController : Controller 
    {

        public ActionResult Index()
        {

            int aktualnaLiczbaWyswietlen = AppGlobalDataContext.LiczbaWyswietlen;
            AppGlobalDataContext.LiczbaWyswietlen = aktualnaLiczbaWyswietlen + 1;

            ViewBag.LiczbaWyswietlenStrony = AppGlobalDataContext.LiczbaWyswietlen;
            ViewBag.navId = 0;

            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }

            using (var db = new MyDbContext())
            {

                var wynik = db.Produkty_w_zamowieniu_goscie
                .GroupBy(zp => zp.ProduktId)
                .Select(g => new
                {
                    IdProduktu = g.Key,
                    SumaIlosci = g.Sum(zp => zp.ilosc)
                })
                .OrderByDescending(x => x.SumaIlosci)
                .Take(10)
                .ToList();

                        if (wynik.Count > 0)
                        {
                            List<ProductListAll> produkty = new List<ProductListAll>();
                        
                            foreach (var item in wynik)
                            {
                            var product = db.Products.FirstOrDefault(p => p.ProduktId == item.IdProduktu && p.isDeleted == false && p.isVisible == true);

                            if (product != null)
                            {
                                var rodzaj = db.Rodzaj.FirstOrDefault(r => r.Id == product.rodzajId);

                                if (rodzaj != null)
                                {
                                    var kategoria = db.Kategoria.FirstOrDefault(k => k.KategoriaId == rodzaj.KategoriaId && k.isDeleted == false && k.isVisible == true);

                                    if (kategoria != null)
                                    {

                                        decimal podatek = db.Podatek.FirstOrDefault(pod => pod.Id == product.vatId).stawka;
                                        if (podatek == null)
                                        {
                                            podatek = 23m;
                                        }

                                        //SPRAWDŹ CZY PRODUKT JEST OBJĘTY PROMOCJĄ

                                        var promocja = db.Promocja_produkt.FirstOrDefault(c => c.ProduktId == product.ProduktId && c.isDeleted == false);
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
                                        string imageUrl = db.Photo.FirstOrDefault(d => d.ProductId == product.ProduktId && d.SectionId == 0).link;
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

                                            produkty.Add( prod );

                                        }
                                        else
                                        {
                                            System.Diagnostics.Debug.WriteLine($"Sprawdzenie 6");

                                        }

                                     }
                                   }
                              }
                            else
                            {
                            }
                          }

                            ViewBag.bestselery = produkty;

                        }
                        else
                        {
                            ViewBag.bestselery = null;
                        }  
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.navId = 3;
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.navId = 4;
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            if (!string.IsNullOrEmpty(TempData["messageSendingForm"] as string))
            {
                ViewBag.response = TempData["messageSendingForm"] as string;
            }
            else
            {
                ViewBag.response = null;
            }

            return View();
        }



    }
}