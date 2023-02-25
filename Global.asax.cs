using MoviesDBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MoviesDBManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DB.Actors.Init(Server.MapPath("~/App_Data/Actors.json"));
            DB.Movies.Init(Server.MapPath("~/App_Data/Movies.json"));
            DB.Castings.Init(Server.MapPath("~/App_Data/Castings.json"));
        }
    }
}
