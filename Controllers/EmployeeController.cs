using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            1 / 0
            return View();
        }
        public ActionResult GetData()
        {
            
            using (DBModel db = new DBModel())
            {
                List<Employee> emplist = db.Employees.ToList<Employee>();
                return Json(new { data = emplist }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            if(id == 0)
            return View(new Employee());
            else
            {
                using (DBModel db=new DBModel())
                {
                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }
            }
        }

        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            using(DBModel db=new DBModel())
            {
                if (emp.EmployeeID == 0)
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db=new DBModel())
            {
                Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault < Employee>();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Succesfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}