using Newtonsoft.Json;
using Sklep.Db_Context;
using Sklep.Models;
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
                Produkt p = _db.Products.SingleOrDefault(p => p.ProduktId == product.ProduktId && p.isVisible == true && p.isDeleted == false);

                string IconPhoto = _db.Photo.SingleOrDefault(p => p.ProductId == product.ProduktId && p.SectionId == 0).link;

                if(string.IsNullOrEmpty(IconPhoto) )
                {
                    IconPhoto = "/Images/NoIcon.PNG";
                }

                if(p != null)
                {
                    decimal cenaBrutto = p.cenaNetto * 1.23m;
                    ProduktWKoszyku pwk = new ProduktWKoszyku()
                    {
                        Produkt = p,
                        Ilosc = product.Liczba,
                        Icon = IconPhoto,
                        StawkaVat = 23m,
                        Wartosc = Math.Ceiling((cenaBrutto * (decimal)product.Liczba) * 100) / 100,
                        CenaBrutto = Math.Ceiling(cenaBrutto * 100) / 100
                    };

                    produkts.Add(pwk);
                    doZaplaty += Math.Ceiling((cenaBrutto * (decimal)product.Liczba) * 100) / 100;
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

                if (existingProduct != null)
                {
                    // Jeśli produkt już istnieje, zwiększ licznik
                    existingProduct.Liczba += pab.Liczba;
                }
                else
                {
                    // Jeśli produkt nie istnieje, dodaj nowy produkt do listy
                    listaProd.Add(pab);
                }
                decimal cena = _db.Products.FirstOrDefault(p=> p.ProduktId == pab.ProduktId).cenaNetto;
                decimal cenaBrutto = cena * 1.23m;

                wartosc += cenaBrutto*pab.Liczba;

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
            HttpCookie wartoscZamowienia = new HttpCookie("KoszykWartosc", (Math.Ceiling(wartosc*100)/100)+"");

            Response.Cookies.Add(cookie);
            Response.Cookies.Add(wartoscZamowienia);

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

    }
}