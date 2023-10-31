using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MktAcademy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MktAcademy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //primeiro método que é aplicado mal a aplicação arranca
        protected void Application_Start()
        {
            //faz logo as alterações da DB antes de começar
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Data.MktAcademyContext, Migrations.Configuration>());
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermissionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissionsToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName("jspdc81@gmail.com");

            if(!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }

            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }

            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }

            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("jspdc81@gmail.com");

            if(user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "jspdc81@gmail.com"
                };

                userManager.Create(user, "abcDEF123!");//password
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            //criar os Roles(RoleManager) ficam armazenados no objeto roleManager
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Apagar um nome mal escrito
            //var role = roleManager.FindByName("Creat");
            //roleManager.Delete(role);

            if(!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
        }
    }
}
