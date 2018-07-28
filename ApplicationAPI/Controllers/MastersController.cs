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

namespace ApplicationAPI.Controllers
{
    public class MastersController : ApiController
    {

        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();

        [HttpGet]
        public List<usp_CylinderMasterGet_Result> GetCylinderMasterList()
        {
            
            List<usp_CylinderMasterGet_Result> cylinderlist = new List<usp_CylinderMasterGet_Result>();
            cylinderlist = InventoryEntities.usp_CylinderMasterGet().ToList();
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
        public List<usp_CylinderMasterMobileGet_Result> GetCylinderMasterMobileList()
        {
            List<usp_CylinderMasterMobileGet_Result> cylinderlist = new List<usp_CylinderMasterMobileGet_Result>();
            cylinderlist = InventoryEntities.usp_CylinderMasterMobileGet().ToList();
            return cylinderlist;
        }
        [HttpGet]
        public usp_CylinderMasterMobileGetByID_Result GetCylinderMasterListMobileByID(string CylindeNumber)
        {
            usp_CylinderMasterMobileGetByID_Result cylinderlist = new usp_CylinderMasterMobileGetByID_Result();
            cylinderlist = InventoryEntities.usp_CylinderMasterMobileGetByID(CylindeNumber).FirstOrDefault();
            return cylinderlist;
        }

        [HttpPost]
        public string CylinderMasterInsertUpdate(usp_CylinderMasterGetByID_Result cylinderModel)
        {

            string result;
            String[] lines = { "First line", "Second line", "Third line" };
            File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);

            result =InventoryEntities.usp_CylinderMasterInsertUpdateMobile(cylinderModel.CylindeNumber, cylinderModel.Barcode, cylinderModel.PresentStateID, cylinderModel.GasInUseID, cylinderModel.VendorBranchID, cylinderModel.Size, cylinderModel.SizeUOMID, cylinderModel.CurrentLocationID
                , cylinderModel.CurrentCustomerBranchID, cylinderModel.Branchid, cylinderModel.CompanyID, cylinderModel.CreatedByID,cylinderModel.UpdatedByID,cylinderModel.status).FirstOrDefault();
            return result;
        }

        [HttpPost]
        public string CylinderMasterInsertMobileUpdate(usp_CylinderMasterMobileGetByID_Result cylinderModel)
        {

            string result;
            //String[] lines = { "First line", "Second line", "Third line" };
            //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
                
            result = InventoryEntities.usp_CylinderMasterInsertUpdateMobile(cylinderModel.CylindeNumber, cylinderModel.Barcode, cylinderModel.PresentStateID, cylinderModel.GasInUseID, cylinderModel.VendorBranchID, cylinderModel.Size, cylinderModel.SizeUOMID, cylinderModel.CurrentLocationID
                , cylinderModel.CurrentCustomerBranchID, cylinderModel.Branchid, cylinderModel.CompanyID, cylinderModel.CreatedByID, cylinderModel.UpdatedByID, cylinderModel.status).FirstOrDefault();
            return result;
        }
        

        [HttpGet]
        public List<usp_CustomerListGet_Result> GetCustomerMobileList()
        {
            List<usp_CustomerListGet_Result> customerlist = new List<usp_CustomerListGet_Result>();
            customerlist = InventoryEntities.usp_CustomerListGet().ToList();
            return customerlist;
        }


        [HttpGet]
        public List<usp_CustomerSiteListGet_Result> GetCustomerMobileSiteList(int customerID)
        {
            List<usp_CustomerSiteListGet_Result> customerSitelist = new List<usp_CustomerSiteListGet_Result>();
            customerSitelist = InventoryEntities.usp_CustomerSiteListGet(customerID).ToList();
            return customerSitelist;
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
            VendorBranchlist = InventoryEntities.usp_VendorBranchListGet(VendorID).ToList();
            return VendorBranchlist;
        }


        [HttpGet]
        public List<usp_VendorBranchListDetailsGet_Result> GetVendorBranchMobileList()
        {
            List<usp_VendorBranchListDetailsGet_Result> Vendorlist = new List<usp_VendorBranchListDetailsGet_Result>();
            Vendorlist = InventoryEntities.usp_VendorBranchListDetailsGet().ToList();
            return Vendorlist;
        }


        [HttpGet]
        public List<usp_CustomrSiteListGet_Result> GetCustomerSiteMobileList()
        {
            List<usp_CustomrSiteListGet_Result> Customerlist = new List<usp_CustomrSiteListGet_Result>();
            Customerlist = InventoryEntities.usp_CustomrSiteListGet().ToList();
            return Customerlist;
        }

        public async Task<string> PostFile()
        {
            // Check if the request contains multipart/form-data.
            //string result;
            //String[] lines = { "First line from file ", "Second line from file", "Third line from file" };
            //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);

        usp_tblStatusMasterGetByType_Result BarcodePath= InventoryEntities.usp_tblStatusMasterGetByType(8).FirstOrDefault();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Content");
            var provider = new MultipartFormDataStreamProvider(BarcodePath.statusDesc.ToString());

            try
            {
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
                return sb.ToString();
            }
            catch (System.Exception e)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                //File.AppendAllText(@"D:\IndoGhana\API\log.txt", e.InnerException.ToString());
                //String[] lines = { "errror", "Second line from file", "Third line from file" };
                //File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);
                return e.InnerException.ToString();
            }
        }


        public async Task<string> PostFileSignature()
        {
            

            usp_tblStatusMasterGetByType_Result SignaturePath = InventoryEntities.usp_tblStatusMasterGetByType(9).FirstOrDefault();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

           // string root = HttpContext.Current.Server.MapPath("~/Content");
            var provider = new MultipartFormDataStreamProvider(SignaturePath.statusDesc.ToString());

            try
            {
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
                File.WriteAllLines(@"D:\IndoGhana\API\log.txt", lines);

              
                return sb.ToString();
            }
            catch (System.Exception e)
            {
               
                return e.InnerException.ToString();
            }
        }
    }
}
