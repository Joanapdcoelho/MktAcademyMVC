using Antlr.Runtime.Tree;
using MktAcademy.Data;
using MktAcademy.Helpers;
using MktAcademy.Models;
using MktAcademy.ViewModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MktAcademy.Controllers
{
    public class OrdersController : Controller
    {
        private MktAcademyContext db = new MktAcademyContext();

        // GET: Orders
        //Index
        public ActionResult NewOrder()
        {            

            var orderView = new OrderView();
            orderView.Customer = new Models.Customer();
            orderView.Courses = new List<CourseOrder>();

            //variável de sessão
            Session["orderView"] = orderView; 
            
            ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");

            return View(orderView); //para fazer uma encomenda nova
        }

        public ActionResult AddCourse()
        {           
            ViewBag.CourseID = new SelectList(CombosHelper.GetCourses(), "CourseID", "Description");

            return View();
        }

        [HttpPost]
        //nova encomenda
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView; //as OrderView é a viewModel OrderView

            var CustomerID = int.Parse(Request["CustomerID"]);
            //caso não haja cliente escolhido
            if (CustomerID == 0)
            {
                ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");
                ViewBag.Error = "You must select a customer!";

                return View(orderView);
            }

            var customer = db.Customers.Find(CustomerID);
            //Verifica se o cliente existe
            if (customer == null)
            {
                ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");
                ViewBag.Error = "The customer doesn't exist!";
                return View(orderView);
            }

            //verificar se já existe 
            if(orderView.Courses.Count == 0)
            {
                ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");
                ViewBag.Error = "You must choose the course to order!";

                return View(orderView);
            }

            //gravar
            int orderID = 0;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    //criar encomenda
                    var order = new Order
                    {
                        CustomerID = CustomerID,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };

                    
                    db.Orders.Add(order);
                    //gravar a encomenda
                    db.SaveChanges();

                    //gravar os detalhes da encomenda
                    orderID = order.OrderID;

                    //int n = 2;
                    //int x = n / 0;

                    foreach (var item in orderView.Courses)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            OrderID = orderID,
                            CourseID = item.CourseID,
                        };

                        db.OrderDetails.Add(orderDetail);
                        //gravar
                        db.SaveChanges();
                    }
                    transaction.Commit();
                }
                
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Error = $"Erro: {ex.Message}";
                    ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");

                    return View(orderView);
                }

                ViewBag.Message = $"The order {orderID} was placed successfully!";

                ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");

                //criar encomenda nova
                orderView = new OrderView();
                orderView.Customer = new Customer();
                orderView.Courses = new List<CourseOrder>();

                Session["orderView"] = orderView;

                return View(orderView);
                //return RedirectToAction("NewOrder");
            }

        }


        [HttpPost]
        public ActionResult AddCourse(CourseOrder courseOrder)
        {
            var orderView = Session["orderView"] as OrderView; //as OrderView é a viewModel OrderView

            var CourseID = int.Parse(Request["CourseID"]); //tem de se passar para inteiro, vem como objeto

            //caso não haja Curso escolhido
            if(CourseID == 0)
            {
                ViewBag.CourseID = new SelectList(CombosHelper.GetCourses(), "CourseID", "Description");
                ViewBag.Error = "You must select a course!";

                return View(courseOrder);
            }

            //Verifica se curso existe
            var Course = db.Courses.Find(CourseID);

            if(Course == null)
            {
                ViewBag.CourseID = new SelectList(CombosHelper.GetCourses(), "CourseID", "Description");
                ViewBag.Error = "The course doesn't exist!";

                return View(courseOrder);
            }

            courseOrder = orderView.Courses.Find(c => c.CourseID == CourseID);//percorrer a lista

            //caso não exista encomenda
            if(courseOrder == null)
            {
                //cria 
                courseOrder = new CourseOrder
                {
                    Description = Course.Description,
                    Price = Course.Price,
                    CourseID = Course.CourseID,
                    Quantity = float.Parse(Request["Quantity"])
                };

                orderView.Courses.Add(courseOrder);
            }

            else
            {
                //vai buscar e adiciona a quantidade
                courseOrder.Quantity += float.Parse(Request["Quantity"]);
            }            

            ViewBag.CustomerID = new SelectList(CombosHelper.GetCustomersName(), "CustomerID", "Name");

            return View("NewOrder", orderView);            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}