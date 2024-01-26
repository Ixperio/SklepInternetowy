using Newtonsoft.Json;
using Sklep.Db_Context;
using Sklep.Models;
using Sklep.Models.Dekorator;
using Sklep.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Sklep.Controllers
{
    public class BusketController : Controller
    {
        private MyDbContext _db;

        public BusketController()
        {
            this._db = MyDbContext.GetInstance();
        }


        public ActionResult Index()
        {
            this.AktualizujWartoscKoszyka();

            List<ProductAddBusket> basket = GetBasketFromCookie();

            List<ProduktWKoszyku> produkts = new List<ProduktWKoszyku>();

            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }

            decimal doZaplaty = 0;

            foreach(ProductAddBusket product in basket)
            {
                Produkt p = _db.Products.FirstOrDefault(p => p.ProduktId == product.ProduktId && p.isVisible == true && p.isDeleted == false);

                if(p != null)
                {
                    var IconPhoto = _db.Photo.FirstOrDefault(ph => ph.ProductId == product.ProduktId && ph.isVisible == true && ph.isDeleted == false);

                    string IconPhotoLink = "";
                    if (IconPhoto == null)
                    {
                        IconPhotoLink = "/Images/NoIcon.PNG";

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(IconPhoto.link))
                        {
                            IconPhotoLink = IconPhoto.link;
                        }
                        else
                        {
                            IconPhotoLink = "/Images/NoIcon.PNG";
                        }

                    }

                    if (p != null)
                    {
                        var podat = _db.Podatek.FirstOrDefault(pod => pod.Id == p.vatId);
                        decimal podatek = 23m;
                        if(podat != null)
                        {
                            podatek = podat.stawka;
                        }

                        decimal cenaNetto = p.cenaNetto;

                        var promocja = _db.Promocja_produkt.FirstOrDefault(prom => prom.ProduktId == p.ProduktId);

                        if(promocja != null)
                        {
                            Product nwProd = new Product(cenaNetto);
                            switch (promocja.PromocjaId)
                            {
                                case 1:
                                    StandardDiscountDecorator standard = new StandardDiscountDecorator(nwProd);
                                    cenaNetto = standard.getPrice();
                                    break;
                                case 2:
                                    HolidayDiscountDecorator holiday = new HolidayDiscountDecorator(nwProd);
                                    cenaNetto = holiday.getPrice();
                                    break;
                                case 3:
                                    SpecialDiscountDecorator special = new SpecialDiscountDecorator(nwProd);
                                    cenaNetto = special.getPrice();
                                    break;
                                default:
                                    break;
                            }
                        }

                        decimal cenaBrutto = cenaNetto * (1+(podatek)/100);

                        ProduktWKoszyku pwk = new ProduktWKoszyku()
                        {
                            Produkt = p,
                            Ilosc = product.Liczba,
                            Icon = IconPhotoLink,
                            StawkaVat = podatek,
                            Wartosc = Math.Ceiling((cenaBrutto * (decimal)product.Liczba) * 100) / 100,
                            CenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100
                        };

                        produkts.Add(pwk);
                        doZaplaty += Math.Ceiling((cenaBrutto * (decimal)product.Liczba) * 100) / 100;
                    }
                }  
            }
            if(produkts.Count > 0)
            {
                ViewBag.ProduktyKoszyka = produkts;
                ViewBag.DoZaplaty = doZaplaty;
            }
            else
            {
                ViewBag.ProduktyKoszyka = null;
                ViewBag.DoZaplaty = 0.0m;
            }

            this.AktualizujWartoscKoszyka();

            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductAddBusket pab)
        {
            if (pab == null)
            {
                // Obsłuż błąd, np. zwróć błąd HTTP 400 Bad Request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            decimal wartosc = 0m;
            List<ProductAddBusket> listaProd = new List<ProductAddBusket>();

            // Sprawdź, czy plik cookie "Koszyk" już istnieje
            if (Request.Cookies["Koszyk"] != null)
            {
                // Jeśli tak, odczytaj jego wartość i zdeserializuj do listy
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;

                // Deserializuj wartość z pliku cookie do listy
                listaProd = JsonConvert.DeserializeObject<List<ProductAddBusket>>(cookieValue);

                // Sprawdź, czy produkt już istnieje w koszyku
                var existingProduct = listaProd.FirstOrDefault(p => p.ProduktId == pab.ProduktId);
                decimal cena = _db.Products.FirstOrDefault(p => p.ProduktId == pab.ProduktId).cenaNetto;
                decimal cenaBrutto = cena * 1.23m;

                if (existingProduct != null)
                {
                    // Jeśli produkt już istnieje, zwiększ licznik
                    wartosc += cenaBrutto * pab.Liczba;
                    existingProduct.Liczba += pab.Liczba;
                }
                else
                {
                    // Jeśli produkt nie istnieje, dodaj nowy produkt do listy
                    wartosc += cenaBrutto * pab.Liczba;
                    listaProd.Add(pab);
                }
            }
            else
            {
                // Jeśli plik cookie "Koszyk" nie istnieje, dodaj nowy produkt do listy
                listaProd.Add(pab);
            }

            // Serializuj listę do postaci tekstowej
            string serializedList = JsonConvert.SerializeObject(listaProd);

            // Utwórz nowy plik cookie lub zaktualizuj istniejący
            HttpCookie cookie = new HttpCookie("Koszyk", serializedList);

            Response.Cookies.Add(cookie);
            this.AktualizujWartoscKoszyka();

            return View("Index");
        }


        [HttpPost]
        public ActionResult Remove(int ProduktId)
        {
            if (ProduktId <= 0)
            {
                // Obsłuż błąd, np. zwróć błąd HTTP 400 Bad Request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<ProductAddBusket> listaProd = new List<ProductAddBusket>();

            // Sprawdź, czy plik cookie "Koszyk" już istnieje
            if (Request.Cookies["Koszyk"] != null)
            {
                // Jeśli tak, odczytaj jego wartość i zdeserializuj do listy
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;

                // Deserializuj wartość z pliku cookie do listy
                listaProd = JsonConvert.DeserializeObject<List<ProductAddBusket>>(cookieValue);

                // Usuń produkt z listy na podstawie ProduktId
                listaProd.RemoveAll(p => p.ProduktId == ProduktId);

                // Serializuj zaktualizowaną listę do postaci tekstowej
                string serializedList = JsonConvert.SerializeObject(listaProd);

                // Utwórz nowy plik cookie lub zaktualizuj istniejący
                HttpCookie cookie = new HttpCookie("Koszyk", serializedList);
                Response.Cookies.Add(cookie);
            }
            this.AktualizujWartoscKoszyka();
            return RedirectToAction("Index"); // Przekieruj do widoku lub innej akcji
        }

        private List<ProductAddBusket> GetBasketFromCookie()
        {
            List<ProductAddBusket> listaProd = new List<ProductAddBusket>();

            // Sprawdź, czy plik cookie "Koszyk" już istnieje
            if (Request.Cookies["Koszyk"] != null)
            {
                // Jeśli tak, odczytaj jego wartość i zdeserializuj do listy
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;

                // Deserializuj wartość z pliku cookie do listy
                listaProd = JsonConvert.DeserializeObject<List<ProductAddBusket>>(cookieValue);
            }
           

            return listaProd;
        }
        /**
         * @brief metoda aktualizująca stan kwoty koszyka
         * @author Artur Leszczak
         */
        public bool AktualizujWartoscKoszyka()
        {
            List<ProductAddBusket> produktyWkoszyku = this.GetBasketFromCookie();
            decimal wartoscKoszyka = 0m;

            foreach(var pb in produktyWkoszyku)
            {
                var produkt = _db.Products.FirstOrDefault(pd => pd.ProduktId == pb.ProduktId && pd.isVisible && !pd.isDeleted);
                

                if(produkt != null)
                {
                    decimal podatek = 23m;

                    var podatek2 = _db.Podatek.FirstOrDefault(pod => pod.Id == produkt.vatId);

                    if (podatek2 != null)
                    {
                        podatek = podatek2.stawka;
                    }

                    var promocja = _db.Promocja_produkt.FirstOrDefault(c => c.ProduktId == produkt.ProduktId && c.isDeleted == false);

                    decimal cenaBrutto = 0m;
                    decimal cenaNetto = produkt.cenaNetto;
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

                    wartoscKoszyka += cenaBrutto * pb.Liczba;
                }
            }
            Response.Cookies.Remove("KoszykWartosc");

            HttpCookie wartoscZamowienia = new HttpCookie("KoszykWartosc", (decimal)(Math.Ceiling(wartoscKoszyka * 100) / 100) + "");
            Response.Cookies.Add(wartoscZamowienia);

            return true;
        }

    }
}