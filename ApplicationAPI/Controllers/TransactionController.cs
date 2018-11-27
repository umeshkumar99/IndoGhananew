using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;
using CylinderAPI.Log;

namespace ApplicationAPI.Controllers
{
    public class TransactionController : ApiController
    {


        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        CreateLogFiles Err = new CreateLogFiles();
        // GET api/<controller>
        public usp_CylinderMasterGetbyBarCode_Result GetCyliderDetailsByBarCode(string barcode)
        {
            usp_CylinderMasterGetbyBarCode_Result cylinderdetails = new usp_CylinderMasterGetbyBarCode_Result();
            try
            {
                Err.ErrorLog("GetCyliderDetailsByBarCode called");
                cylinderdetails = InventoryEntities.usp_CylinderMasterGetbyBarCode(barcode).FirstOrDefault();
                Err.ErrorLog("GetCyliderDetailsByBarCode called end");
                return cylinderdetails;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCyliderDetailsByBarCode Error:" + ex.Message);
                return cylinderdetails;
            }

        }

        public int MobileWarningMsgInsert(AlertMessageTrackModel alertMessageTrackModel)
        {

            try
            {
                InventoryEntities.usp_MobileWarningMsgInsert(              alertMessageTrackModel.TransactionMode,       alertMessageTrackModel.CylinderNumber,
   alertMessageTrackModel.DateTime,       alertMessageTrackModel.CompanyID,
       alertMessageTrackModel.BranchID,        alertMessageTrackModel.UserID,
           alertMessageTrackModel.CurrentCustomerBranchID,         alertMessageTrackModel.CustomerName,
           alertMessageTrackModel.MessageDescription,      alertMessageTrackModel.MessageLocation


                    );
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
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