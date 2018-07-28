using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;

namespace CylinderAPI.Controllers
{
    public class LoginController : ApiController
    {
        // GET api/<controller>
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();


        [HttpGet]
        public USP_GetUserDetails_Result CheckLoginStatus(string mobileno="", string username = "", string pwd="")
        {
            //string   pwd="";
            USP_GetUserDetails_Result userdetails = InventoryEntities.USP_GetUserDetails(username, pwd, mobileno).FirstOrDefault();
            return userdetails;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}