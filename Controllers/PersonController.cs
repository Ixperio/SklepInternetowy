
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
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }
        /**
         * @autor Artur Leszczak
         * @description Funckja Register pozwala na rejestrację nowego użytkownika
         */
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
        public ActionResult Register(PersonRegister person)
        {

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
        public ActionResult Login(PersonLogin person)
        {
            ViewBag.Message = person.Email + " " + person.Password;

            return View();
        }
    }
}