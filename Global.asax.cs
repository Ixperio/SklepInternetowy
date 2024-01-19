using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

using Sklep.Models;

namespace Sklep
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /**
             * @brief Inicjalizacja bazy danych
             * 
             * @author <span><ul><li>Artur Leszczak</li><li>Katarzyna Grygo</li></ul></span>
             */

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());
            MyDbContext context = new MyDbContext();
            context.Database.Initialize(true);

        }
    }
}
