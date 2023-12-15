using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    public class ShoppingCartController : Controller
    {
        public LayersEntities db = new LayersEntities();
        // GET: ShoppingCart

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }
        public ActionResult AddtoCart(int ID)
        {
            var product = db.Products.SingleOrDefault(s => s.ID_product == ID);
            if (product != null)
            {
                GetCart().Add(product);
            }

            return RedirectToAction("Show", "ShoppingCart");

        }
       
        public ActionResult Show()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Show", "ShoppingCart");
            }
            Cart cart = Session["Cart"] as Cart;
          
            
            return View(cart);
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id = int.Parse(form["ID_product"]);
            int quantity = int.Parse(form["Quantity"]);

            cart.Update_Quanlity_Shopping(id, quantity);
            return RedirectToAction("show", "ShoppingCart");
        }

        public ActionResult letgo()
        {

            return View();

        }
        public ActionResult Payment()
        {
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        [HttpPost]
        public ActionResult Payment(FormCollection form)
        {
            try
            {
            Cart cart = Session["Cart"] as Cart;


                Order order = new Order();
                order.Orderdate = DateTime.Now.ToString();
                order.Cus_Name = form["Cus_Name"];
                order.Phone = form["Phone"];
                order.Address = form["Address"];

                db.Orders.Add(order);
                foreach (var item in cart.Items)
                {
                    Orderdetail orderdetail = new Orderdetail();
                    orderdetail.ID_order = order.ID_order;
                    orderdetail.Price = item.quanlity * item.products.Price;
                    orderdetail.Quantity = Convert.ToInt32(item.quanlity);
                    orderdetail.ID_product = item.products.ID_product;
                    db.Orderdetails.Add(orderdetail);
                   

                } 
                db.SaveChanges();
                cart.clearCart();




                return RedirectToAction("letgo", "ShoppingCart");

            }

            catch
            {
                return Content("error");
            }
           

        }
        //public ActionResult Test()
        //{
        //    var model = new CartView().lala().ToList();
        //    var model1 = new CartView().lalo().ToList();

        //    dynamic mymodel = new ExpandoObject();
        //    mymodel.Model = model;
        //    mymodel.Model1 = model1;
        //    return View(mymodel);
        //}

    }

}
