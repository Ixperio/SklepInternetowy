using Sklep.Db_Context;
using Sklep.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sklep.Observer;
using System.Xml.Linq;


namespace Sklep.Controllers
{
    public class ContactController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendQuestionExpert(ContactFormView cfv, int produktId)
        {
            if(ModelState.IsValid)
            {
                EmailNotification newMail = new EmailNotification();
                ContactFormExpertView contactFormExpertView = new ContactFormExpertView()
                {
                    email = cfv.email,
                    message = cfv.message,
                    name = cfv.name,
                    ProductId = produktId
                };
                
                newMail.InfoDoExperta(contactFormExpertView);

                TempData["messageSendingForm"] = "Dziękujemy za kontakt, wkrótce ekspert skontaktuje się z Państwem na przekazany adres email.";
               return RedirectToAction("Details", "Product", new { id = produktId });
            }
            else
            {
                TempData["messageSendingForm"] = "Nie udało się wysłać! - Nieprawidłowy format danych";
               return RedirectToAction("Details", "Product", new { id = produktId });
            }
           

        }

    }
}