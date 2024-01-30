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
        private MyDbContext _db;

        public ContactController(MyDbContext myDb) { 
            _db = myDb;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}