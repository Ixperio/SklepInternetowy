using Sklep.Db_Context;
using Sklep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Sklep.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string name = collection["name"];
                string description = collection["description"];
                //string price = collection["price"];

                ProductBuilder builder = new ProductBuilder();
                builder.Reset();
                builder.BuildName(name);
                builder.BuildDescription(new Opis());
                //builder.BuildPrice(price.AsDecimal());

                Produkt produkt = builder.GetProduct();
                MyDbContext _db = DbContextConnectionSingleton.GetInstance().MyDbContext;
                _db.Products.Add(produkt);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
