using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CylnderEntities;
using CylinderAPI.Log;
using System.IO;
using Microsoft.VisualBasic;
using System.Text;
using System.Threading.Tasks;

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

        public int MobileWarningMsgInsert(List<AlertMessageTrackModel> alertMessageTrackModel)
        {

            try
            {
                int? result = 0;
                Err.ErrorLog("MobileWarningMsgInsert called");
                foreach (AlertMessageTrackModel alertMessageTrackModel1 in alertMessageTrackModel)
                {
                     InventoryEntities.usp_MobileWarningMsgInsert(alertMessageTrackModel1.TransactionMode, alertMessageTrackModel1.CylinderNumber,
             alertMessageTrackModel1.DateTime, alertMessageTrackModel1.CompanyID,      alertMessageTrackModel1.BranchID, alertMessageTrackModel1.UserID,
           alertMessageTrackModel1.CurrentCustomerBranchID, alertMessageTrackModel1.CustomerName,
           alertMessageTrackModel1.MessageDescription, alertMessageTrackModel1.MessageLocation
                    );
                }

                Err.ErrorLog("MobileWarningMsgInsert call end");
                return 1;
            }
            catch(Exception ex)
            {
                Err.ErrorLog("MobileWarningMsgInsert error");
                return 0;
            }
        }


        [HttpPost]
        public int InsertTransactionAllDetailCylinderRecieve(List<TransactionAllDetailCylinderRecieve> transactionAllDetailCylinderRecieve)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("InsertTransactionAllDetailCylinderRecieve called");
                foreach (TransactionAllDetailCylinderRecieve trans in transactionAllDetailCylinderRecieve)
                {
                    result = (int)InventoryEntities.usp_tblReceivedAllTransactionDetailsInsert(trans.TransactionNumber, trans.TransactionMode, trans.SourceCylinderID, Convert.ToByte(trans.flgSourceBarCodeExists), trans.SourceBarCodeNumber, trans.SourceCylinderNumber, trans.SourceCylinderSize, trans.TargetCylinderID, Convert.ToByte(trans.flgTargetBarCodeExists), trans.TargetBarCodeNumber, trans.TargetCylinderNumber, trans.TargetCylinderSize, Convert.ToByte(trans.Sstat), trans.CustomerID, trans.CurrentCustomerBranchID, trans.CustomerName, trans.VendorName, trans.SizeUOM, trans.PresentState, trans.PresentStateID, trans.LocationID, trans.VanBatchNumber, trans.TransactionDateTime, trans.CompanyID, trans.BranchID, trans.UserID, trans.GasInUse).FirstOrDefault();


                }
                Err.ErrorLog("InsertTransactionAllDetailCylinderRecieve call ended");
                return result;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("InsertTransactionAllDetailCylinderRecieve Error:" + ex.Message);
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
                    result = (int)InventoryEntities.usp_DayStartDayEndInsert(trans.DayStartDateTime, trans.DayEndDateTime, trans.ForDate, trans.Sstat, trans.CompanyID, trans.BranchID, trans.UserID, trans.logid, trans.IMEI).FirstOrDefault();


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

        public int InsertReceivedCylinderSign(List<CylinderRecieveSign> cylinderRecieveSign)
        {
            try
            {
                int result = 0;
                Err.ErrorLog("InsertReceivedCylinderSign called");
                foreach (CylinderRecieveSign trans in cylinderRecieveSign)
                {
                    result = (int)InventoryEntities.usp_ReceivedCylinderSign(trans.TransactionNumber, trans.CustomerSignature, trans.logid, trans.ForDate, trans.CurrentDateTime, trans.CompanyID, trans.BranchID, trans.UserID, trans.Sstat, trans.Remarks).FirstOrDefault();


                }
                Err.ErrorLog("InsertReceivedCylinderSign call Ended");
                return result;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("InsertReceivedCylinderSign Error:" + ex.Message);
                return 0;
            }
        }


        //method to save file for  Signature VanOffload
        public async Task<string> PostFileVanOffload()
        {

            try
            {
                Err.ErrorLog("PostFileVanOffload called");
                usp_tblStatusMasterGetByType_Result SignaturePath = InventoryEntities.usp_tblStatusMasterGetByType(15).FirstOrDefault();
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                // string root = HttpContext.Current.Server.MapPath("~/Content");
                var provider = new MultipartFormDataStreamProvider(SignaturePath.statusDesc.ToString());


                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                string filename = ""; int count = 0;
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                        if (count == 0)
                        {
                            filename = val;
                        }
                    }
                }

                // This illustrates how to get the file names for uploaded files.


                foreach (var file in provider.FileData)
                {
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    FileSystem.Rename(fileInfo.FullName, SignaturePath.statusDesc.ToString() + filename);
                    //sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                }

                String[] lines = { "sucess sb string" };
                //      File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);

                Err.ErrorLog("PostFileVanOffload called end");
                return "1";
            }
            catch (System.Exception e)
            {
                Err.ErrorLog("PostFileVanOffload Error:" + e.Message);
                return "0";
            }
        }

    }
}