using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        // GET: Admin/Customer
        public LayersEntities db = new LayersEntities();

        public ActionResult Index(string search ="")
        {
            //Search
            List<Employee> employee = db.Employees.Where(row => row.Name.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(employee);
        }
        public ActionResult AddEmployee()

        {
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }


        [HttpPost]
        public ActionResult AddEmployee(string Username, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Username = form["Username"];
                var emp = db.Employees.FirstOrDefault(row => row.Username == Username);
                if (emp == null)
                {
                    Employee employee = new Employee();
                    employee.Name = form["Name"];
                    employee.Address = form["Address"];
                    employee.Birthday = form["Birthday"];
                    employee.Gender = form["Gender"];
                    employee.Phone = form["Phone"];
                    employee.Gmail = form["Gmail"];
                    employee.Username = form["Username"];
                    employee.Password = form["Password"];
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    return RedirectToAction("AddEmployee","Employee");

                }
            }
            return View();

        }






        public ActionResult EditEmployee(int id)
        {
            Employee emp = db.Employees.Where(row => row.ID_employee == id).FirstOrDefault();
            return View(emp);
        }

        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {
            Employee employee = db.Employees.Where(row => row.ID_employee == emp.ID_employee).FirstOrDefault();
            if (employee != null)
            {
                employee.Name = emp.Name;
                employee.Address = emp.Address;
                employee.Birthday = emp.Birthday;
                employee.Phone = emp.Phone;
                employee.Gmail = emp.Gmail;
                employee.Username = emp.Username;

                db.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View("EditEmployee");
            }
        }

        public ActionResult Delete(int id)
        {
            Employee emp = db.Employees.Where(row => row.ID_employee == id).FirstOrDefault();
            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {

            Employee emp = db.Employees.Where(row => row.ID_employee == id).FirstOrDefault();
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Delete", "Employee");
        }
    }
}