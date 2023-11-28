using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sklep.Models.ModelViews;
using Sklep.Models;
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
            this._db = DbContextConnectionSingleton.GetInstance().MyDbContext;
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
        //[HttpPost]
       /* public ActionResult Register(PersonRegistration personRegistered)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Utworzono nowe konto!";
                /*Person person = new Person();
                person.Email = personRegistered.Email;
                person.Phone = personRegistered.PhoneNumber;
                person.Name = personRegistered.FirstName;
                person.Surname = personRegistered.LastName;
                person.Birthday = personRegistered.BirthDate;
                person.Logowanie = new Logowanie()
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