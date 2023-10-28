using MktAcademy.Data;
using MktAcademy.Helpers;
using MktAcademy.ViewModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MktAcademy.Controllers
{
    public class OrdersController : Controller
    {
        private MktAcademyContext db = new MktAcademyContext();
        // GET: Orders
        public ActionResult NewOrder()
        {            

            var orderView = new OrderView();
            orderView.Customer = new Models.Customer();
            orderView.Courses = new List<CourseOrder>();
            
            ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");

            return View(orderView); //para fazer uma encomenda nova
        }

        public ActionResult AddCourse()
        {
            var list = db.Courses.ToList();
            list.Add(new CourseOrder { CourseID = 0, Description = "Select a Course..." });

            ViewBag.CourseID = new SelectList(list.OrderBy(c => c.Description).ToList(), "CourseID", "Description");

            return View();
        }
    }
}