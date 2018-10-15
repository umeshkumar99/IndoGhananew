using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using CylnderEntities;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using CylinderAPI.Log;


namespace ApplicationAPI.Controllers
{
    public class MastersController : ApiController
    {

        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        CreateLogFiles Err = new CreateLogFiles();

        [HttpGet]
        public List<usp_CylinderMasterGet_Result> GetCylinderMasterList(int branchid, int companyid)
        {
            
            List<usp_CylinderMasterGet_Result> cylinderlist = new List<usp_CylinderMasterGet_Result>();
            cylinderlist = InventoryEntities.usp_CylinderMasterGet(branchid,companyid).ToList();
            return cylinderlist;
        }
        [HttpGet]
        public usp_CylinderMasterGetByID_Result GetCylinderMasterListByID(string CylindeNumber )
        {
            usp_CylinderMasterGetByID_Result cylinderlist = new usp_CylinderMasterGetByID_Result();
            cylinderlist = InventoryEntities.usp_CylinderMasterGetByID(CylindeNumber).FirstOrDefault();
            return cylinderlist;
        }


        [HttpGet]
        public List<usp_CylinderMasterMobileGet_Result> GetCylinderMasterMobileList(int branchid, int companyid)
        {
            List<usp_CylinderMasterMobileGet_Result> cylinderlist = new List<usp_CylinderMasterMobileGet_Result>();
            try
            {
                Err.ErrorLog("GetCylinderMasterMobileList called");
                cylinderlist = InventoryEntities.usp_CylinderMasterMobileGet(branchid,companyid).ToList();
                Err.ErrorLog("GetCylinderMasterMobileList called end");
                return cylinderlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCylinderMasterMobileList Error:" + ex.Message);
                return cylinderlist;
            }
        }
        [HttpGet]
        public usp_CylinderMasterMobileGetByID_Result GetCylinderMasterListMobileByID(string CylindeNumber)
        {
            usp_CylinderMasterMobileGetByID_Result cylinderlist = new usp_CylinderMasterMobileGetByID_Result();
            try
            {
                Err.ErrorLog("GetCylinderMasterListMobileByID called");
                cylinderlist = InventoryEntities.usp_CylinderMasterMobileGetByID(CylindeNumber).FirstOrDefault();
                Err.ErrorLog("GetCylinderMasterListMobileByID called end");
                return cylinderlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCylinderMasterListMobileByID Error:" + ex.Message);
                return cylinderlist;
            }
        }

        [HttpPost]
        public string CylinderMasterInsertUpdate(usp_CylinderMasterGetByID_Result cylinderModel)
        {

            string result=string.Empty;
            String[] lines = { "First line", "Second line", "Third line" };
            //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
            try
            {
                Err.ErrorLog("CylinderMasterInsertUpdate called");

                result = InventoryEntities.usp_CylinderMasterInsertUpdate(cylinderModel.CylindeNumber, cylinderModel.Barcode, cylinderModel.ManufacturerID, cylinderModel.PurchaseDate
                , cylinderModel.InitialGasID, cylinderModel.WLCapacity, cylinderModel.WLCapacityUOMID, cylinderModel.WorkingPressure, cylinderModel.WorkingPressureUOMID,
                cylinderModel.TestDate, cylinderModel.NextTestDate, cylinderModel.ValveModelID, cylinderModel.PresentStateID,
                cylinderModel.GasInUseID, cylinderModel.VendorBranchID, cylinderModel.Size, cylinderModel.SizeUOMID, cylinderModel.CurrentLocationID
                , cylinderModel.CurrentCustomerBranchID, cylinderModel.Branchid, cylinderModel.CompanyID, cylinderModel.CreatedByID,
                cylinderModel.UpdatedByID, cylinderModel.status).FirstOrDefault();
                Err.ErrorLog("CylinderMasterInsertUpdate called end");
                return result;
               
            }
            catch (Exception ex)
            {
                Err.ErrorLog("CylinderMasterInsertUpdate Error:" + ex.Message);
                return result;
            }
        }

        [HttpPost]
        public string CylinderMasterInsertMobileUpdate(usp_CylinderMasterMobileGetByID_Result cylinderModel)
        {
            string result;
            try
            {

                //String[] lines = { "First line", "Second line", "Third line" };
                //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
                Err.ErrorLog("CylinderMasterInsertMobileUpdate called");
                result = InventoryEntities.usp_CylinderMasterInsertUpdateMobile(cylinderModel.CylindeNumber, cylinderModel.Barcode, cylinderModel.PresentStateID, cylinderModel.GasInUseID, cylinderModel.VendorBranchID, cylinderModel.Size, cylinderModel.SizeUOMID, cylinderModel.CurrentLocationID
                    , cylinderModel.CurrentCustomerBranchID, cylinderModel.Branchid, cylinderModel.CompanyID, cylinderModel.CreatedByID, cylinderModel.UpdatedByID, cylinderModel.status, cylinderModel.IsPrintDone).FirstOrDefault();
                Err.ErrorLog("CylinderMasterInsertMobileUpdate called end");
                return result;
            }
            catch(Exception ex)
            {
                Err.ErrorLog("CylinderMasterInsertMobileUpdate Error:" + ex.Message);
                return ex.InnerException.Message;
            }
        }
        

        [HttpGet]
        public List<usp_CustomerListGet_Result> GetCustomerMobileList()
        {
            List<usp_CustomerListGet_Result> customerlist = new List<usp_CustomerListGet_Result>();
            try
            {
                Err.ErrorLog("GetCustomerMobileList called");
                customerlist = InventoryEntities.usp_CustomerListGet().ToList();
                Err.ErrorLog("GetCustomerMobileList called end");
                return customerlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCustomerMobileList Error:" + ex.Message);
                return customerlist;
            }
        }


        [HttpGet]
        public List<usp_CustomerSiteListGet_Result> GetCustomerMobileSiteList(int customerID)
        {
            List<usp_CustomerSiteListGet_Result> customerSitelist = new List<usp_CustomerSiteListGet_Result>();
            try
            {
                Err.ErrorLog("GetCustomerMobileSiteList called");
                customerSitelist = InventoryEntities.usp_CustomerSiteListGet(customerID).ToList();
                Err.ErrorLog("GetCustomerMobileSiteList called end");
                return customerSitelist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCustomerMobileSiteList Error:" + ex.Message);
                return customerSitelist;
            }
        }



        [HttpGet]
        public List<usp_VendorListGet_Result> GetVendorMobileList()
        {
            List<usp_VendorListGet_Result> Vendorlist = new List<usp_VendorListGet_Result>();
            Vendorlist = InventoryEntities.usp_VendorListGet().ToList();
            return Vendorlist;
        }


        [HttpGet]
        public List<usp_VendorBranchListGet_Result> GetVendorBranchMobileList(int VendorID)
        {
            List<usp_VendorBranchListGet_Result> VendorBranchlist = new List<usp_VendorBranchListGet_Result>();
            try
            {
                Err.ErrorLog("GetVendorBranchMobileList called");
                VendorBranchlist = InventoryEntities.usp_VendorBranchListGet(VendorID).ToList();
                Err.ErrorLog("GetVendorBranchMobileList called end");
                return VendorBranchlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetVendorBranchMobileList Error:" + ex.Message);
                return VendorBranchlist;
            }
        }


        [HttpGet]
        public List<usp_VendorBranchListDetailsGet_Result> GetVendorBranchMobileList()
        {
            List<usp_VendorBranchListDetailsGet_Result> Vendorlist = new List<usp_VendorBranchListDetailsGet_Result>();
            try
            {
                Err.ErrorLog("GetVendorBranchMobileList called");
                Vendorlist = InventoryEntities.usp_VendorBranchListDetailsGet().ToList();
                Err.ErrorLog("GetVendorBranchMobileList called end");
                return Vendorlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetVendorBranchMobileList Error:" + ex.Message);
                return Vendorlist;
            }
        }


        [HttpGet]
        public List<usp_CustomrSiteListGet_Result> GetCustomerSiteMobileList()
        {
            List<usp_CustomrSiteListGet_Result> Customerlist = new List<usp_CustomrSiteListGet_Result>();
            try
            {
                Err.ErrorLog("GetCustomerSiteMobileList called");
                Customerlist = InventoryEntities.usp_CustomrSiteListGet().ToList();
                Err.ErrorLog("GetCustomerSiteMobileList called end");
                return Customerlist;
            }
            catch (Exception ex)
            {
                Err.ErrorLog("GetCustomerSiteMobileList Error:" + ex.Message);
                return Customerlist;
            }
        }

        //method to save file for barcode
        public async Task<string> PostFile()
        {
            // Check if the request contains multipart/form-data.
            //string result;
            //String[] lines = { "First line from file ", "Second line from file", "Third line from file" };
            //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
            try
            {
                Err.ErrorLog("Upload Barcode Image called");

                usp_tblStatusMasterGetByType_Result BarcodePath= InventoryEntities.usp_tblStatusMasterGetByType(8).FirstOrDefault();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content");
            var provider = new MultipartFormDataStreamProvider(BarcodePath.statusDesc.ToString());

           
                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                string filename=""; int count = 0;
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
                    FileSystem.Rename(fileInfo.FullName, BarcodePath.statusDesc.ToString()  + filename);    
                    //sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                }

                //if (!String.IsNullOrEmpty(sb.ToString()))
                //{
                //    //File.AppendAllText(@"D:\IndoGhana\API\log.txt", sb.ToString());
                    String[] lines = { "sucess sb string" };
                    File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);

                //}
                //else
                //{
                //    String[] lines = { "empty sb string", "Second line from file", "Third line from file" };
                //    File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
                //    //   File.AppendAllText(@"D:\IndoGhana\API\log.txt", "No key found");
                //}
                ////return new HttpResponseMessage()
                //{
                //    Content = new StringContent(sb.ToString())
                //};

                //    return "Successfully uploaded";
                Err.ErrorLog("Upload Barcode Image called end");
                return sb.ToString();
            }
            catch (System.Exception e)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                //File.AppendAllText(@"D:\IndoGhana\API\log.txt", e.InnerException.ToString());
                //String[] lines = { "errror", "Second line from file", "Third line from file" };
                //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
                Err.ErrorLog("Barcode Image Error:" + e.Message);
                return e.InnerException.ToString();
            }
        }

        //method to save file for  Signature
        public async Task<string> PostFileSignature()
        {

            try
            {
                Err.ErrorLog("PostFileSignature called");
                usp_tblStatusMasterGetByType_Result SignaturePath = InventoryEntities.usp_tblStatusMasterGetByType(9).FirstOrDefault();
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

                Err.ErrorLog("PostFileSignature called end");
                return "1";
            }
            catch (System.Exception e)
            {
                Err.ErrorLog("PostFileSignature Error:" + e.Message);
                return "0";
            }
        }
    }
}
