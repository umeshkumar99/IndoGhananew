using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CylnderEntities;
using System.Net;

namespace IndoGhana
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        string IPAddress;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start()
        {
            
        }
        protected void Session_End()
        {
            USP_GetUserDetails_Result logindetails;
            //if (Session["logindetails"] != null)
            //{
            logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
            // }
            IPAddress = GetIPAddress();
                InventoryEntities.usp_UpdateLogoutTime(logindetails.logid, IPAddress);
        }


        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

    }
}
