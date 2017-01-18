using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace HRUTWeb
{
    public class Global : HttpApplication
    {
        public static string ServerRoot { private set; get; }
        public static PhotoHandler PhotoHandler { private set; get; }
        public static Random Random { private set; get; }

        void Application_Start(object sender, EventArgs e)
        {
            ServerRoot = Server.MapPath("~");
            Logger.SetConfig(ServerRoot);

            Logger.WriteLog("Startup");

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            PhotoHandler = new PhotoHandler();
            Random = new Random();
        }
    }
}