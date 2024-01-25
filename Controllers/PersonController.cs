using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sklep.Models.ModelViews;
using Sklep.Models;

using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

using Sklep.Db_Context;


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
            this._db = MyDbContext.GetInstance();
        }

        // GET: Person
        public ActionResult Index()
        {
            if (Request.Cookies["Koszyk"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        /**
       * Prezentacja widoku
       */
        [HttpGet]
        public ActionResult Register()
        {
            if (Request.Cookies["Koszyk"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["Koszyk"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }
        /**
         * Pobieranie danych z formularza
         */
        [HttpPost]
        public ActionResult Register(PersonRegistration personRegistered)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Utworzono nowe konto!";
                Person person = new Person()
                {
                    Email = personRegistered.Email,
                    Phone = personRegistered.PhoneNumber,
                    Name = personRegistered.FirstName,
                    Surname = personRegistered.LastName,
                    Birthday = personRegistered.BirthDate,
                    LogowanieId = 0, 
                    AccountTypeId = 0 
                };

                person.Logowanie = new Logowanie
                {
                    Login = personRegistered.Login,
                    Password = personRegistered.Password
                };

                _db.Logowanie.Add(person.Logowanie);
                _db.Person.Add(person);
                _db.SaveChanges();
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
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }
        /**
         * Pobieranie danych z formularza
         */
        [HttpPost]
        public ActionResult Login(PersonLogin personLogged)
        {
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            if (ModelState.IsValid)
            {

                var query = from c in _db.Logowanie where c.Login == personLogged.Login && c.Password == personLogged.Password select c;

                var user = query.FirstOrDefault();

                if (user != null)
                {
                    Session["UserId"] = user.LogowanieId;
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