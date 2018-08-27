using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;
using  CylinderAPI.Log;

namespace CylinderAPI.Controllers
{
    public class CommanController : ApiController
    {

        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        CreateLogFiles Err = new CreateLogFiles();
        [HttpGet]
        public string Index()
        {
            return "Hi";
        }
        // GET api/<controller>
        public List<usp_tblStatusMasterGetByType_Result> GetDropDownValues(int statusTypeID )
        {
            List<usp_tblStatusMasterGetByType_Result> statusList=new List<usp_tblStatusMasterGetByType_Result>();
            try
            {
                
                Err.ErrorLog( "GetDropDownValues called");
               //Err.ErrorLog(Server.MapPath("../Logs/ErrorLog"), "");
                statusList = InventoryEntities.usp_tblStatusMasterGetByType(statusTypeID).ToList();
                Err.ErrorLog("GetDropDownValues called end");
                return statusList;
            }
            
            catch (Exception ex)
            {
                Err.ErrorLog("GetDropDownValues Error:"+ ex.Message);
                return statusList;
            }
            
        }
        [HttpPost]
        public int GetBatchStartEnd(List<BatchStartEnd> batchStartEnd)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("batchStartEnd called");
                foreach (BatchStartEnd batch in batchStartEnd)
                {
                     result = (int)InventoryEntities.usp_tblBatchStartEndInsert(batch.VanBatchNumber, batch.BatchStartDateTime, batch.BatchEndDatetime, batch.ForDate, batch.Sstat, batch.CompanyID, batch.BranchID, batch.UserID).FirstOrDefault();
                }
                Err.ErrorLog("batchStartEnd call Ended");
                return result;
            }
            catch(Exception ex)
            {
                Err.ErrorLog("batchStartEnd Error:" +  ex.Message);
                return 0;
            }
        }
        [HttpPost]
        public int GetCylinderInVan(List<CylinderInVan> cylinderInVan)
        {
            try
            {
                int result=0;
                Err.ErrorLog("cylinderInVan called");
                foreach (CylinderInVan cylinder in cylinderInVan)
                {
                    result = (int)InventoryEntities.usp_tblCylinderInVanInsert(cylinder.vehicleID, cylinder.initialSize, cylinder.remainingsizeforRefill, cylinder.vanBatchNumber, cylinder.VendorName, cylinder.cylinderLoadingDateTime, cylinder.cylinderID, cylinder.cylinderNumber, cylinder.cylinderVendorID, cylinder.cylinderBranchID, cylinder.sstat, cylinder.transactionPoint, cylinder.LocationID, cylinder.TransType, cylinder.CylinderStatus, cylinder.OwnerId, cylinder.vendorBranchID, cylinder.CompanyID, cylinder.BranchID, cylinder.UserID).FirstOrDefault();
                }
                Err.ErrorLog("cylinderInVan call Ended");
                return result;
            }
            catch(Exception ex)
            {
                Err.ErrorLog("batchStartEnd Error:" + ex.Message);
                return 0;
            }
        }

        [HttpPost]
        public int GetTransactionAllDetail(List<TransactionAllDetail> transactionAllDetail)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("transactionAllDetail called");
                foreach (TransactionAllDetail trans in transactionAllDetail)
                {
                    result = (int)InventoryEntities.usp_tblTransactionAllDetailInsert(trans.TransactionNumber, trans.TransactionMode, trans.SourceCylinderID, Convert.ToByte(trans.flgSourceBarCodeExists), trans.SourceBarCodeNumber, trans.SourceCylinderNumber, trans.SourceCylinderSize, trans.TargetCylinderID, Convert.ToByte(trans.flgTargetBarCodeExists), trans.TargetBarCodeNumber, trans.TargetCylinderNumber, trans.TargetCylinderSize, Convert.ToByte(trans.Sstat), trans.CustomerID, trans.CurrentCustomerBranchID, trans.CustomerName, trans.VendorName, trans.SizeUOM, trans.PresentState, trans.PresentStateID, trans.LocationID, trans.VanBatchNumber, trans.TransactionDateTime, trans.CompanyID, trans.BranchID, trans.UserID, trans.GasInUse).FirstOrDefault();


                }
                Err.ErrorLog("transactionAllDetail call ended");
                return result;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("transactionAllDetail Error:" + ex.Message);
                return 0;
            }
        }



        [HttpPost]
        public int GetDayStartDayEnd(List<DayStartDayEndTable> dayStartDayEndTable)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("dayStartDayEndTable called");
                foreach (DayStartDayEndTable trans in dayStartDayEndTable)
                {
                    result = (int)InventoryEntities.usp_DayStartDayEndInsert(trans.DayStartDateTime,trans.DayEndDateTime,trans.ForDate,trans.Sstat, trans.CompanyID, trans.BranchID, trans.UserID,trans.logid,trans.IMEI).FirstOrDefault();


                }
                Err.ErrorLog("dayStartDayEndTable call Ended");
                return result;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("dayStartDayEndTable Error:" + ex.Message);
                return 0;
            }
        }


        [HttpPost]

        public int GetCustomerTransactionSign(List<CustomerTransactionSign> customerTransactionSign)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("customerTransactionSign called");
                foreach (CustomerTransactionSign trans in customerTransactionSign)
                {
                    result = (int)InventoryEntities.usp_CustomerTransactionSignInsert(trans.TransactionNumber,trans.CustomerID,trans.CurrentCustomerBranchID, trans.IsSatisfied,trans.CustomerSignature,trans.logid,trans.ForDate,trans.CurrentDateTime, trans.CompanyID, trans.BranchID, trans.UserID, trans.Sstat,trans.Remarks).FirstOrDefault();


                }
                Err.ErrorLog("customerTransactionSign call Ended");
                return result;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("customerTransactionSign Error:" + ex.Message);
                return 0;
            }
        }

    }
}