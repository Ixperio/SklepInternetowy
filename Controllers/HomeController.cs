using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sklep.Models.ModelViews;
using Sklep.Models.Utils;
using Sklep.Observer; //zawiera kontekst globalny

namespace Sklep.Controllers
{


    public class HomeController : Controller 
    {

        public ActionResult Index()
        {

            int aktualnaLiczbaWyswietlen = AppGlobalDataContext.LiczbaWyswietlen;
            AppGlobalDataContext.LiczbaWyswietlen = aktualnaLiczbaWyswietlen + 1;

            ViewBag.LiczbaWyswietlenStrony = AppGlobalDataContext.LiczbaWyswietlen;
            ViewBag.navId = 0;

            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
              
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.navId = 3;
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.navId = 4;
            if (Request.Cookies["KoszykWartosc"] != null)
            {
                HttpCookie existingCookie = Request.Cookies["KoszykWartosc"];
                string cookieValue = existingCookie.Value;
                ViewBag.WartoscKoszyka = cookieValue;
            }
            if (!string.IsNullOrEmpty(TempData["messageSendingForm"] as string))
            {
                ViewBag.response = TempData["messageSendingForm"] as string;
            }
            else
            {
                ViewBag.response = null;
            }

            return View();
        }



    }
}