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

namespace Sklep.Controllers
{
    public class OrderController : Controller
    {

        private MyDbContext _db;

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

        [HttpGet]
        public ActionResult Index()
        {
            List<KurierView> kurierViews = new List<KurierView>();

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

            //ZWRÓCENIE WSZYSTKICH DOSTĘPNYCH FORM DOSTAWY
            ViewBag.kurierzy = kurierViews;

            return View();
        }

        [HttpPost]
        public ActionResult Podsumowanie(OrderView order)
        {



        }


    }
}