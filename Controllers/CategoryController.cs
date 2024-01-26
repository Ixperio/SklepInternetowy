using Sklep.Db_Context;
using Sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sklep.Controllers
{
    public class CategoryController : Controller
    {

        private MyDbContext _db;

        public CategoryController()
        {
            this._db = MyDbContext.GetInstance();
        }

        // GET: Category
        public ActionResult Index()
        {
            var kategorie = _db.Kategoria.Where(c=> c.isVisible == true && c.isDeleted == false).ToList();

            ViewBag.Category = kategorie;  
            return View();
        }

        public ActionResult Add(Kategoria kat)
        {
            return View();
        }
        public ActionResult Hide(int id)
        {
            return View();
        }

    }
}