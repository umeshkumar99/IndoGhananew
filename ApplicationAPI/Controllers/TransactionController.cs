using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;

namespace ApplicationAPI.Controllers
{
    public class TransactionController : ApiController
    {


        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        // GET api/<controller>
        public usp_CylinderMasterGetbyBarCode_Result GetCyliderDetailsByBarCode(string barcode)
        {

            usp_CylinderMasterGetbyBarCode_Result cylinderdetails = InventoryEntities.usp_CylinderMasterGetbyBarCode(barcode).FirstOrDefault();
            return cylinderdetails;

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