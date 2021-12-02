using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace DKdll
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DKbase.Helper.getConnectionStringSQL = ConfigurationManager.ConnectionStrings["db_conexion"].ConnectionString;
            DKbase.Helper.getFolder = @"C:\LogTiempoWebService\";
            DKbase.Helper.getTipoApp = "DLL";
        }
    }
}