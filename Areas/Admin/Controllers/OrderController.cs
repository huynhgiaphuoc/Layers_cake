using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public LayersEntities db = new LayersEntities();

        // GET: Admin/Order
        public ActionResult Index(string search="")
        {  //Search
            List<Order> order = db.Orders.Where(row => row.Cus_Name.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(order);
        }
        public ActionResult EditOrder(int id)
        {
            Order ord = db.Orders.Where(row => row.ID_order == id).FirstOrDefault();
            return View(ord);
        }

        [HttpPost]
        public ActionResult EditOrder(Order ord)
        {
            Order order = db.Orders.Where(row => row.ID_order == ord.ID_order).FirstOrDefault();
            if (order != null)
            {
                order.Cus_Name = ord.Cus_Name;
                order.Address = ord.Address;
                order.Phone = ord.Phone;

                db.SaveChanges();
                return RedirectToAction("Index", "Order");
            }
            else
            {
                return View("EditOrder");
            }
        }
    }
}