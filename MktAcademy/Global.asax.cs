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
        protected void Application_Start()
        {
            //faz logo as alterações da DB antes de começar
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Data.MktAcademyContext, Migrations.Configuration>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
