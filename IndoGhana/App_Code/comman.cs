using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndoGhana.App_Code
{
    public class comman : Controller
    {

        public comman()
        {
            if (Session["userdetails"] == null)
            {
                Session.Abandon();
                RedirectToAction("Index", "Login", new { area = "Login" });
            }
        }
    }
}