using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep.Controllers
{
    public class HomeController : Controller 
    {

        public ActionResult Index()
        {

            int aktualnaLiczbaWyswietlen = (int)HttpContext.Application["liczba_wyswietlen"];
            HttpContext.Application["liczba_wyswietlen"] = aktualnaLiczbaWyswietlen + 1;

            ViewBag.LiczbaWyswietlenStrony = HttpContext.Application["liczba_wyswietlen"];

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}