using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DKdll
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            DKbase.Helper.getConnectionStringSQL = "";
            DKbase.Helper.getPathSiteWeb = @"C:\LogTiempoWebService\";
            DKbase.Helper.getFolderLog = "Log";
            DKbase.Helper.getTipoApp = "DLL";
        }
    }
}