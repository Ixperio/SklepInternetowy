
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SklepInternetowy.Models;

namespace SklepInternetowy.Controllers
{
    public class PersonController : Controller
    {
        /**
        * @autor Artur Leszczak
        * @description Funckja Register pozwala na rejestrację nowego użytkownika
        */

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
        [HttpPost]
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
            ViewBag.Message = personLogged.Email + " " + personLogged.Password;

            return View();
        }
    }
}