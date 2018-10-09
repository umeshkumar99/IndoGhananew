using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;
using CylinderAPI.Log;

namespace CylinderAPI.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/<controller>

        CreateLogFiles Err = new CreateLogFiles();
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();


        [HttpGet]
        public USP_GetUserDetails_Result CheckLoginStatus(string mobileno="", string username = "", string pwd="")
        {
            //string   pwd="";
            USP_GetUserDetails_Result userdetails=new USP_GetUserDetails_Result();
            try
            {
                Err.ErrorLog("CheckLoginStatus called");
                userdetails = InventoryEntities.USP_GetUserDetails(username, pwd, mobileno).FirstOrDefault();
                Err.ErrorLog("CheckLoginStatus called end");
                return userdetails;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("CheckLoginStatus Error:" + ex.Message);
                return userdetails;
            }
        }

    }
}