using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MktAcademy.Models;
using MktAcademy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MktAcademy.Controllers
{
    public class UsersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //variável users
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();

            //passar 1 a 1
            foreach(var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    UserID = user.Id
                };

                usersView.Add(userView);
            }

            return View(usersView);
        }

        //Get: Roles
        public ActionResult Roles(string userID)
        {

            if(string.IsNullOrEmpty (userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var roles = roleManager.Roles.ToList();
            var users = userManager.Users.ToList();
            //trazer o Id do user 
            var user = users.Find(u => u.Id == userID);

            if(user == null)
            {
                return HttpNotFound();
            }

            var rolesView = new List<RoleView>();
            foreach(var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                };

                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id,
                Roles = rolesView

            };

            return View(userView);

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