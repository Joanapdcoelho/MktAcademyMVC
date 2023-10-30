using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MktAcademy.Data;
using MktAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MktAcademy.Helpers
{
    public class CombosHelper
    {
        
        private static MktAcademyContext db = new MktAcademyContext();

        private static ApplicationDbContext da = new ApplicationDbContext();


        public static List<DocumentType> GetDocumentTypes()
        {
            var DocumentTypes = db.DocumentTypes.ToList();
            DocumentTypes.Add(new DocumentType
            {
                DocumentTypeID = 0,
                Description = "[Select a type of document]"
            }) ;

            return DocumentTypes.OrderBy(d => d.Description).ToList();
        }

        public static List<Customer> GetCustomersName()
        {
            var Customers = db.Customers.ToList();
            Customers.Add(new Customer 
            { 
                CustomerID = 0, 
                CustomerFirstName = "[Select a customer]" 
            });

            return Customers.OrderBy(c => c.Name).ToList();
        }

        public static List<Course> GetCourses()
        {
            var Courses = db.Courses.ToList();
            Courses.Add(new Course
            {
                CourseID = 0,
                Description = "Select a Course..."
            });

            return Courses.OrderBy(c => c.Description).ToList();
        }

        public static List<IdentityRole> GetRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(da));
            var list = roleManager.Roles.ToList();//transformar em lista
            list.Add(new IdentityRole { Id = "", Name = "[Select a permission...]" });
            return list.OrderBy(r => r.Name).ToList();
        }

        public void Dispose() 
        { 
            db.Dispose(); 
        }


    }
}