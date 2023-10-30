using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MktAcademy.Helpers;
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

        //Post: Roles
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();
            //trazer o Id do user 
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id
            };

                       
            ViewBag.RoleID = new SelectList(CombosHelper.GetRoles(), "Id", "Name");

            return View(userView);

        }

        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            //vai buscar o ID
            var roleID = Request["RoleID"];

            //ver todos os users
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //carregar todos os users - userManager
            var users = userManager.Users.ToList();
            //ir buscar aquele user que se quer
            var user = users.Find(u => u.Id == userID);

            //criar objeto userView
            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                UserID = user.Id
            };

            //para o caso de não selecionar nada na combobox
            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "You must select a permission!";
                ViewBag.RoleID = new SelectList(CombosHelper.GetRoles(), "Id", "Name");
                return View(userView);
            }

            //carregar todos os roles RoleManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //define a lista dos roles
            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r=> r.Id == roleID);

            //se não tiver a permissão atribui
            if (!userManager.IsInRole(userID, role.Name))
            {
                userManager.AddToRole(userID, role.Name);//adiciona o role que não tinha à tabela
            }

            //criar lista de roles
            var rolesView = new List<RoleView>();

            //passar os roles todos para o user
            foreach (var item in user.Roles)
            {
                //vai buscar
                role = roles.Find(r => r.Id == item.RoleId);
                //faz o role
                var roleView = new RoleView
                {
                    RoleName = role.Name,
                    RoleID = role.Id
                };

                //adiciona o roleView à lista
                rolesView.Add(roleView);
            }

            //passar o userView para o modelo adiciona e já vai estar preenchido 
            userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesView,
                UserID = user.Id
            };


            return View("Roles", userView);

        }

        public ActionResult Delete(string userID, string roleID)
        {
            if(string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            

            if (userID == null || roleID == null) 
            {
                return HttpNotFound();
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            //trazer o Id do user 
            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(u => u.Id == roleID);

            //apagar o user deste role
            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            //preparar a view
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach(var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleName = role.Name,
                    RoleID = role.Id
                };

                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = rolesView,
                UserID = user.Id
            };

            return View("Roles", userView);

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