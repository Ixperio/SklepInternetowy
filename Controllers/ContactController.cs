using Sklep.Db_Context;
using Sklep.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sklep.Observer;


namespace Sklep.Controllers
{
    public class ContactController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SendQuestion(ContactFormView cfv)
        {

            if (ModelState.IsValid)
            {

                EmailNotification newMail = new EmailNotification();
                newMail.InfoZeStrony(cfv);

                TempData["messageSendingForm"] = "Dziękujemy za kontakt, wkrótce ktoś się z Państwem skontaktuje na przekazany adres email.";

            }
            else
            {
                TempData["messageSendingForm"] = "Nie udało się wysłać! - Nieprawidłowy format danych";
            }

            return RedirectToAction("Contact", "Home");
        }
    }
}