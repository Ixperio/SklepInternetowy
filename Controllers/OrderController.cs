using Sklep.Models;
using System.Web.Mvc;

using Sklep.Models.ModelViews;

namespace Sklep.Controllers
{
    public class OrderController : Controller
    {

        private readonly MyDbContext _db;

        public OrderController()
        {
            this._db = new MyDbContext();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Order/SubmitOrder.cshtml");
        }

        [HttpPost]
        [Route("/order")]
        public ActionResult Order(OrderSubmit orderSubmit)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Złożono zamówienie!";
            }
            else
            {
                ViewBag.Message = "Wprowadzono nieprawidłowe dane!";
            }

            return View();
        }

    }
}
