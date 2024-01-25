using Sklep.Models.Metoda_wytworcza;
using Sklep.Models.Metoda_wytworcza.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {

           IFactoryDostawa dostawyFabrykaKurier = new DostawaKurier();
           IFactoryDostawa dostawyFabrykaKurierPobranie = new DostawaPobranieKurier();
           IDostawa dostawa = dostawyFabrykaKurier.createFactory();
           IDostawa dostawaPobranie = dostawyFabrykaKurierPobranie.createFactory();

            ViewBag.kurier = dostawa.getName()+" "+ dostawa.getPrice() + "zł";
            ViewBag.kurierPobranie = dostawaPobranie.getName()+" "+ dostawaPobranie.getPrice()+"zł";

            return View();
        }
    }
}