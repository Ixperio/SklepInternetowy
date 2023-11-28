﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sklep.Models.ModelViews;
using Sklep.Models;

namespace Sklep.Controllers
{
    public class PersonController : Controller
    {
        private readonly MyDbContext _db;

        /**
        * @autor Artur Leszczak
        * @description Funckja Register pozwala na rejestrację nowego użytkownika
        */
         
        public PersonController() {
             this._db = new MyDbContext();
        }

        // GET: Person
        public ActionResult Index()
        {
            return View();
        }

        /**
       * Prezentacja widoku
       */
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        /**
         * Pobieranie danych z formularza
         */
     /*   [HttpPost]
        public ActionResult Register(PersonRegister personRegistered)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Utworzono nowe konto!";
            }
            else
            {
                ViewBag.Message = "Wprowadzono nieprawidłowe dane!";
            }
            return View();
        }

        /*
        * @autor Artur Leszczak
        * @description Funckja Login pozwala na zalogowanie się istniejącego użytkownika
        */

        /**
        * Prezentacja widoku
        */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /**
         * Pobieranie danych z formularza
         */
        [HttpPost]
        public ActionResult Login(PersonLogin personLogged)
        {
            if (ModelState.IsValid)
            {

                var query = from c in _db.Logowanie where c.Login == personLogged.Login && c.Password == personLogged.Password select c;
 
                if (query.ToList().Count != 0 ) {

                    ViewBag.Message = "Zalogowano";
                }
                else
                {
                    ViewBag.Message = "Niepoprawne dane!";
                }
               
            }
            

            return View();
        }
    }
}