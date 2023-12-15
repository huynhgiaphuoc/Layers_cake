using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        public LayersEntities db = new LayersEntities();
        // GET: Product
        public ActionResult Index()
        {
            List<Product> product = db.Products.ToList();
            List<Category> categories = db.Categories.ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Product = product;
            mymodel.Category = categories;
            return View(mymodel);

            
          
        }
        public ActionResult details()
        {
            return View();
        }


    }
}