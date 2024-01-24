﻿using Sklep.Models;
using System.Web.Mvc;

using Sklep.Models.ModelViews;
using Sklep.Db_Context;
using Sklep.Observer;
using System;

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
                    Country = orderSubmit.Country
                };

                _db.Adress.Add(adress);
                _db.SaveChanges();

                EmailNotification notification = new EmailNotification();
                Zamowienia zamowienia = new Zamowienia();
                zamowienia.Attach(notification);

                zamowienia.SetStatus("Przyjeto do realizacji"); //korzystac z setstatus kiedy chce wyslac powiadomienie
                ViewBag.Message = "Złożono zamówienie!";
            }
            else
            {
                ViewBag.Message = "Wprowadzono nieprawidłowe dane!";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Cart()
        {
            return View("~/Views/Order/Cart.cshtml");
        }

        [HttpPost]
        public ActionResult Cart(OrderCart orderCart)
        {
            return View();
        }

    }
}