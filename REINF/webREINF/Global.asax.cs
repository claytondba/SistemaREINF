using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace webREINF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Código que é executado na inicialização do aplicativo
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Add("usuario", null);
            Session.Add("active_page", null);
            Session.Add("edit_reg", false);
            Session.Add("edit_usuario", false);
            Session.Timeout = 5;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Session["usuario"] = null;
            Session["active_page"] = null;
            Session.Clear();
            //Response.Redirect("Login.aspx");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}