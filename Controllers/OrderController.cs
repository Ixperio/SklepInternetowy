using Sklep.Models;
using System.Web.Mvc;

using Sklep.Models.ModelViews;
using Sklep.Db_Context;
using Sklep.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using Sklep.Models.Metoda_wytworcza.Interface;
using Sklep.Models.Metoda_wytworcza;
using System.EnterpriseServices;
using System.Web;
using Newtonsoft.Json;

namespace Sklep.Controllers
{
    public class OrderController : Controller
    {

        private MyDbContext _db;
        private List<KurierView> kurierViews;
        public OrderController()
        {
            this._db = MyDbContext.GetInstance();
        }

        [HttpGet]
        public ActionResult SubmitOrder()
        {
            return View("~/Views/Order/SubmitOrder.cshtml");
        }

        [HttpPost]
        public ActionResult SubmitOrder(OrderSubmit orderSubmit)
        {
            if (ModelState.IsValid)
            {
                Adress adress = new Adress
                {
                    Street = orderSubmit.Street,
                    HomeNumber = orderSubmit.Number,
                    FlatNumber = orderSubmit.Flat_Number,
                    City = orderSubmit.City,
                    PostCode = orderSubmit.PostalCode,
                    State = orderSubmit.State,
                    Country = orderSubmit.Country,
                    Email = orderSubmit.Email
                };


                var x = _db.Adress.Add(adress);
                _db.SaveChanges();

                EmailNotification notification = new EmailNotification();
                ZamowieniaKlienci zamowienia = new ZamowieniaKlienci();
                zamowienia.adresId = x.AdressId;
                zamowienia.Attach(notification);

                zamowienia.SetStatus("Przyjeto do realizacji"); //korzystac z setstatus kiedy chce wyslac powiadomienie
                ViewBag.Message = "Złożono zamówienie!";

                _db.Zamowienia.Add(zamowienia);
                _db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "Wprowadzono nieprawidłowe dane!";
            }

            return View();
        }

        [HttpGet]
        public ActionResult CheckOrders()
        {
            if (Session["UserId"] != null)
            {
                int userId = (int)Session["UserId"];
                var user = _db.Person.Find(userId);

                if (user != null && user.AccountTypeId == 2)
                {
                    List<ZamowieniaKlienci> orders = _db.Zamowienia.ToList();

                    return View("~/Views/Order/CheckOrders.cshtml", orders);
                }
            }

            return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult CheckOrders(FormCollection collection)
        {
            return View();
        }

        private void tworzKurierow()
        {
            kurierViews = new List<KurierView>();

            //ZASTOSOWANIE FABRYKI KURIERÓW 
            IFactoryDostawa kurierzyZwykli = new DostawaKurier();

            List<IDostawa> kuriers = kurierzyZwykli.createFactory();

            foreach (var kurier in kuriers)
            {
                kurierViews.Add(new KurierView() { Nazwa = kurier.getName(), Cena = kurier.getPrice() });
            }
            //ZASTOSOWANIE FABRYKI KURIERÓW POBRANIOWYCH
            IFactoryDostawa kurierzyPobranie = new DostawaPobranieKurier();
            List<IDostawa> kuriersPobranie = kurierzyPobranie.createFactory();

            foreach (var kurier in kuriersPobranie)
            {
                kurierViews.Add(new KurierView() { Nazwa = kurier.getName(), Cena = kurier.getPrice() });
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            this.tworzKurierow();

            //ZWRÓCENIE WSZYSTKICH DOSTĘPNYCH FORM DOSTAWY
            ViewBag.kurierzy = kurierViews;

            using (var db = new MyDbContext())
            {
                var platnosci = db.Rodzaj_platnosci.Where(c => c.IsVisible == true && c.IsDeleted == false).ToList();
                if (platnosci != null)
                {
                    ViewBag.platnosci = platnosci;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Podsumowanie(OrderView order)
        {
            if(ModelState.IsValid)
            {
                decimal cena_zamowienia = 0m;
                BusketController controller = new BusketController();
                    HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                    cena_zamowienia = decimal.Parse(existingCookie.Value);

                this.tworzKurierow();
                decimal cena_kuriera = this.kurierViews.ElementAt(order.DostawaId-1).Cena;

                List<ProductAddBusket> listaProd = new List<ProductAddBusket>();

                // Sprawdź, czy plik cookie "Koszyk" już istnieje
                if (Request.Cookies["Koszyk"] != null)
                {
                    // Jeśli tak, odczytaj jego wartość i zdeserializuj do listy
                    HttpCookie existingCookieKoszyk = Request.Cookies["Koszyk"];
                    string cookieValue = existingCookieKoszyk.Value;

                    // Deserializuj wartość z pliku cookie do listy
                    listaProd = JsonConvert.DeserializeObject<List<ProductAddBusket>>(cookieValue);
                }

                //utwórz zamówienie
                int idZamowienia; // Przechowuje Id zamówienia

                using (var db = _db)
                {
                    ZamowieniaGoscie zg = new ZamowieniaGoscie()
                    {
                        Imie = order.Imie,
                        Nazwisko = order.Nazwisko,
                        Adres = "ul. " + order.Street + " " + order.NumerDomu + " " + order.Post + " " + order.Town,
                        Phone = order.Phone,
                        dostawaId = order.DostawaId,
                        addDate = DateTime.Now,
                        AdresEmail = order.Email,
                        platnosc_typId = order.PlatnoscId,
                        walutaId = 1,
                        kwota = cena_zamowienia + cena_kuriera,
                        status = "Przyjęte"
                    };

                    _db.ZamowieniaGoscie.Add(zg);
                    _db.SaveChanges();

                    idZamowienia = zg.Id;

                    foreach (var p in listaProd)
                    {
                        Produkty_w_zamowieniu_goscie pwg = new Produkty_w_zamowieniu_goscie()
                        {
                            ProduktId = p.ProduktId,
                            ilosc = p.Liczba,
                            zamowienie_goscieId = idZamowienia
                        };

                        _db.Produkty_w_zamowieniu_goscie.Add(pwg);
                        _db.SaveChanges();
                    }
                }

                if (idZamowienia != null)
                {
                    ViewBag.nrZamowienia = idZamowienia;
                    ViewBag.kwota = cena_zamowienia + cena_kuriera;
                }
                else
                {
                    ViewBag.nrZamowienia = "Brak";
                    ViewBag.kwota = "Brak";
                }
                return View();
            }
            else
            {
                return View("Index");
            }
        }
    }
}