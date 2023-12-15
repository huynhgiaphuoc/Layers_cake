using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public LayersEntities db = new LayersEntities();    
        // GET: Admin/Product
       
            public ActionResult Index(string search = " ")
            {
                //Search
                List<Product> product = db.Products.Where(row => row.Name.Contains(search)).ToList();
                ViewBag.Search = search;



                return View(product);
            }


           
        public ActionResult AddProduct()

        {
            List<Category> cate = db.Categories.ToList();
            List<Product> pro = db.Products.ToList();
            dynamic la = new ExpandoObject();
            la.Category = cate;
            la.Product = pro;
            return View(la);  
        }
      

        [HttpPost]
        public ActionResult AddProduct(Product pro, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                string filename = pro.Name + ".jpg";
                string pu = Path.Combine(Server.MapPath("~/Content/imglayers/banhman/"), filename);
                imageFile.SaveAs(pu);
                pro.Image = filename;
                db.Products.Add(pro);
                db.SaveChanges();
                return RedirectToAction("Index","Product");
            }
            else
            {
                return View("AddProduct");
            }

        }
        public ActionResult EditProduct(int id)
        {
            Product pro = db.Products.Where(row => row.ID_product == id).FirstOrDefault();
            return View(pro);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase imageFiles)
        {
            Product pro = db.Products.Where(row => row.ID_product == product.ID_product).FirstOrDefault();
            if (pro != null)
            {
                pro.Name = product.Name;
                pro.Price = product.Price;
                pro.Description = product.Description;
                pro.Details= product.Details;
                pro.saled= product.saled;
                pro.Total_quantity = product.Total_quantity;
                pro.ID_category = product.ID_category;
                string filename = pro.Name + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Content/imglayers/banhman/"),filename);
                imageFiles.SaveAs(path);

               pro.Image = filename;
                db.SaveChanges();
                return RedirectToAction("Index","Product");
            }
            else
            {
                return View("EditProduct");
            }
        }

        public ActionResult Delete(int id)
        {
            Product pro = db.Products.Where(row => row.ID_product == id).FirstOrDefault();
            return View(pro);
        }

        [HttpPost]
        public ActionResult Delete(int id,Product product)
        {
            Product pro = db.Products.Where(row => row.ID_product == id).FirstOrDefault();
                db.Products.Remove(pro);           
                db.SaveChanges(); 
            return RedirectToAction("Delete","Product");
        }
       
        // GET: Admin/Products
     
        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == 3)
            {
                TempData["AlertType"] = "alert-danger";
            }
            else
            {
                TempData["AlertType"] = "alert-info";
            }
        }

    }
}