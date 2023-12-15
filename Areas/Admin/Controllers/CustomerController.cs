using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        public LayersEntities db = new LayersEntities();

        public ActionResult Index(string search = "")
        {
            //Search
            List<Customer> customers = db.Customers.Where(row => row.Name.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(customers);
        }
        public ActionResult AddCustomer()

        {
            List<Customer> cus = db.Customers.ToList();
        
          
            return View(cus);
        }


        [HttpPost]
        public ActionResult AddCustomer(string Username,FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Username = form["Username"];
                var cus = db.Customers.FirstOrDefault(row => row.Username == Username);
                    if(cus == null) {
                    Customer customer = new Customer();
                    customer.Name = form["Name"];
                    customer.Address = form["Address"];
                    customer.Birthday = form["Birthday"];
                    customer.Gender = form["Gender"];
                    customer.Phone = form["Phone"];
                    customer.Gmail = form["Gmail"];
                    customer.Username = form["Username"];
                    customer.Password = form["Password"];
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                   
                    return RedirectToAction("AddCCustomer","Customer");
                }
            }
                   return View();

            }

                
                
            

        
        public ActionResult EditCustomer(int id)
        {
            Customer cus = db.Customers.Where(row => row.ID_customer == id).FirstOrDefault();
            return View(cus);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            Customer cus = db.Customers.Where(row => row.ID_customer== customer.ID_customer).FirstOrDefault();
            if (cus != null)
            {
                cus.Name = customer.Name;
              cus.Address = customer.Address;
                cus.Birthday= customer.Birthday;
                cus.Phone = customer.Phone;
           

                cus.Gmail = customer.Gmail;
                cus.Username= customer.Username;
                
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View("EditCustomer");
            }
        }

        public ActionResult Delete(int id)
        {
            Customer cus = db.Customers.Where(row => row.ID_customer == id).FirstOrDefault();
            return View(cus);
        }

        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {
            Customer cus = db.Customers.Where(row => row.ID_customer == id).FirstOrDefault();
            db.Customers.Remove(cus);
            db.SaveChanges();
            return RedirectToAction("Delete", "Customer");
        }

        // GET: Admin/Products

    }
}