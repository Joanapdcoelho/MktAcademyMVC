using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MktAcademy.Data;
using MktAcademy.Models;

namespace MktAcademy.Controllers
{
    //autorizar um utilizador específico
    //[Authorize(Users = "leonorpimenta32@gmail.com")]
    // para entrar no controlador é preciso autorização
    //[Authorize]
    public class CoursesController : Controller
    {
        private MktAcademyContext db = new MktAcademyContext();


        // GET: Courses
        [Authorize(Roles = "View")]

        //public ActionResult Index()
        //{       
        //    return View(db.Courses.ToList());            
        //}
        
        public ActionResult Index(string searchCourse, string searchPrice)
        {            
            //buscar todos 
            var courses = from c in db.Courses select c;            

            //filtrar 
            if (!string.IsNullOrEmpty(searchCourse))
            {
                courses = courses.Where(s => s.Name.Contains(searchCourse));
                //price = price.Where(s => s.Price.Contains(searchPrice));
            }

            //return View(db.Courses.ToList());
            return View(courses.ToList());
        }               

        // GET: Courses/Details/5
        [Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = db.Courses.Find(id);

            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //[Authorize]
        // GET: Courses/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Name,Description,Price,LastBuy,Stock,Area,Remarks")] Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.LastBuy > DateTime.Today)
                {
                    ModelState.AddModelError("LastBuy", "The date cannot be later than the current day.");
                    return View(course);
                }

                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(course);
        }

        //[Authorize]
        // GET: Courses/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Name,Description,Price,LastBuy,Stock,Area,Remarks")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        //[Authorize]
        // GET: Courses/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //fechar a conexão
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
