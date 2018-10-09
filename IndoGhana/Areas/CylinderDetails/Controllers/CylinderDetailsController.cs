using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using ZXing.OneD;
using ZXing;
using Microsoft.Reporting.WebForms;

namespace IndoGhana.Areas.CylinderDetails.Controllers
{
  //  [Authorize]
    public class CylinderDetailsController : Controller
    {
       
        
        // GET: CylinderDetails/CylinderDetails
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        USP_GetUserDetails_Result logindetails;
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            return View();
        }

        [HttpGet]
     //   [OutputCache(Duration =1000)]
        public ActionResult GetCylinderList()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            List<usp_CylinderMasterGet_Result> cylinderlist = new List<usp_CylinderMasterGet_Result>();
            logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
            try
            {

                cylinderlist = InventoryEntities.usp_CylinderMasterGet(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                return Json(cylinderlist, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Edit(string cylindernumber)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            usp_CylinderMasterGetByID_Result cylinderDetails = new usp_CylinderMasterGetByID_Result();
            try
            {
                cylinderDetails = InventoryEntities.usp_CylinderMasterGetByID(cylindernumber).FirstOrDefault();
                FillViewBag();
                ViewBag.VendorBranchID = new SelectList(InventoryEntities.usp_VendorBranchListGet(cylinderDetails.VendorID), "VendorBranchID", "VendorBranchName");
                return View(cylinderDetails);
            }

            catch (Exception ex)
            {
                return View();
            }
        }





        [HttpPost]
        public ActionResult Edit(FormCollection frm)
        {

            try
            {
                usp_CylinderMasterGetByID_Result cylinder = new usp_CylinderMasterGetByID_Result();
                TryUpdateModel(cylinder);
               // USP_GetUserDetails_Result logindetails;
              
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                //bool b= int.TryParse(frm["WLCapacityUOMID"], out re);
                string barcode = cylinder.CylindeNumber.ToString() + cylinder.VendorID.ToString() + cylinder.VendorBranchID.ToString();
                object result = InventoryEntities.usp_CylinderMasterInsertUpdate(cylinder.CylindeNumber, barcode, cylinder.ManufacturerID,
                    cylinder.PurchaseDate, cylinder.InitialGasID, cylinder.WLCapacity,
                    cylinder.WLCapacityUOMID, cylinder.WorkingPressure, cylinder.WorkingPressureUOMID,
                    cylinder.TestDate, cylinder.NextTestDate, cylinder.ValveModelID,
                    cylinder.PresentStateID, cylinder.GasInUseID, cylinder.VendorBranchID,
                    cylinder.Size, cylinder.SizeUOMID, cylinder.CurrentLocationID, cylinder.CurrentCustomerBranchID, logindetails.Branch_Id, logindetails.Company_Id, logindetails.USer_Id, logindetails.USer_Id, cylinder.status);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Detail(string cylindernumber)
        {
            usp_CylinderMasterGetByID_Result cylinderDetails = new usp_CylinderMasterGetByID_Result();
            cylinderDetails = InventoryEntities.usp_CylinderMasterGetByID(cylindernumber).FirstOrDefault();
            return View(cylinderDetails);
        }
        public ActionResult printbarcode(string cylindernumber)
        {
            usp_CylinderMasterGetBarcodeImage_Result barcodeImage = new usp_CylinderMasterGetBarcodeImage_Result();
            barcodeImage = InventoryEntities.usp_CylinderMasterGetBarcodeImage(cylindernumber).FirstOrDefault();
            return View(barcodeImage);
        }
        [HttpGet]
        public ActionResult CreateCylinderDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            FillViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateCylinderDetails(FormCollection frm)
        {
            try
            {
                usp_CylinderMasterGetByID_Result cylinder = new usp_CylinderMasterGetByID_Result();
                TryUpdateModel(cylinder);
                //bool b= int.TryParse(frm["WLCapacityUOMID"], out re);
                string barcode = cylinder.CylindeNumber.ToString() + cylinder.VendorID.ToString() + cylinder.VendorBranchID.ToString();
                

                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                object result = InventoryEntities.usp_CylinderMasterInsertUpdate(cylinder.CylindeNumber, barcode, cylinder.ManufacturerID,
                    cylinder.PurchaseDate, cylinder.InitialGasID, cylinder.WLCapacity,
                    cylinder.WLCapacityUOMID, cylinder.WorkingPressure, cylinder.WorkingPressureUOMID,
                    cylinder.TestDate, cylinder.NextTestDate, cylinder.ValveModelID,
                    cylinder.PresentStateID, cylinder.GasInUseID, cylinder.VendorBranchID,
                    cylinder.Size, cylinder.SizeUOMID, cylinder.CurrentLocationID, cylinder.CurrentCustomerBranchID, logindetails.Branch_Id, logindetails.Company_Id, logindetails.USer_Id, logindetails.USer_Id, cylinder.status);
                GenerateBarcode(barcode);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        private void FillViewBag()
        {
            ViewBag.InitialGasID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(4), "StatusID", "statusDesc");
            ViewBag.WLCapacityUOMID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(7), "StatusID", "statusDesc");
            ViewBag.WorkingPressureUOMID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(7), "StatusID", "statusDesc");
            ViewBag.ValveModelID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(5), "StatusID", "statusDesc");
            ViewBag.PresentStateID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(1), "StatusID", "statusDesc");
            ViewBag.GasInUseID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(4), "StatusID", "statusDesc");
            ViewBag.SizeUOMID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(7), "StatusID", "statusDesc");
            ViewBag.CurrentLocationID = new SelectList(InventoryEntities.usp_tblStatusMasterGetByType(13), "StatusID", "statusDesc");
            ViewBag.VendorID = new SelectList(InventoryEntities.usp_VendorListGet(), "VendorID", "VendorName");
            ViewBag.CustomerID = new SelectList(InventoryEntities.usp_CustomerListGet(), "CustomerID", "CustomerName");



            ViewBag.ManufacturerID = new SelectList(InventoryEntities.usp_ManufacturerMasterGet(), "ManufacturerID", "ManufacturerName");

        }

        public JsonResult getBranch(int id)
        {
            List<SelectListItem> BranchSelectList = new List<SelectListItem>();
            List<usp_VendorBranchListGet_Result> BranchList = new List<usp_VendorBranchListGet_Result>();
            //AddressModel address = new AddressModel();
            BranchList = InventoryEntities.usp_VendorBranchListGet(id).ToList();
            BranchList.ForEach(x =>
            {
                BranchSelectList.Add(new SelectListItem { Text = x.VendorBranchName, Value = x.VendorBranchID.ToString() });
            });
            return Json(new SelectList(BranchSelectList, "Value", "Text", JsonRequestBehavior.AllowGet));
        }


        public JsonResult getCustomerBranch(int id)
        {
            List<SelectListItem> BranchSelectList = new List<SelectListItem>();
            List<usp_CustomerSiteListGet_Result> BranchList = new List<usp_CustomerSiteListGet_Result>();
            //AddressModel address = new AddressModel();
            BranchList = InventoryEntities.usp_CustomerSiteListGet(id).ToList();
            BranchList.ForEach(x =>
            {
                BranchSelectList.Add(new SelectListItem { Text = x.SiteName, Value = x.CustomerSiteID.ToString() });
            });
            return Json(new SelectList(BranchSelectList, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        private void GenerateBarcode(string barcode)
        {
            try
            {
                //


                var writer = new ZXing.BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128
                };
                Bitmap bitmap = writer.Write(barcode);

                usp_tblStatusMasterGetByType_Result result  = InventoryEntities.usp_tblStatusMasterGetByType(8).FirstOrDefault();
                string sPath = result.statusDesc;
               

                using (MemoryStream Mmst = new MemoryStream())
                {


                    //bitm.Save("ms", ImageFormat.Jpeg);
                    bitmap.Save(sPath + barcode + ".png");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult ReportCylinderDashboard()
        {
            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
              //  USP_GetUserDetails_Result logindetails;
                 logindetails = (USP_GetUserDetails_Result)Session["logindetails"];

                //for cylinder count location wise
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;

                List<usp_CylinderCount_Result> CylinderListCount = new List<usp_CylinderCount_Result>();
             
                CylinderListCount = InventoryEntities.usp_CylinderCount(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderCount.rdlc";
                reportview.ShowToolBar = false;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderListCount));

                List<usp_CylinderCountPState_Result> CylinderCountPState = new List<usp_CylinderCountPState_Result>();

                CylinderCountPState = InventoryEntities.usp_CylinderCountPState(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                //reportviewPstate.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderCountPState.rdlc";

                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", CylinderCountPState));

                List<usp_CylinderCountGasInUse_Result> CylinderCountGasInUse = new List<usp_CylinderCountGasInUse_Result>();

                CylinderCountGasInUse = InventoryEntities.usp_CylinderCountGasInUse(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                //reportviewPstate.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderCountPState.rdlc";

                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", CylinderCountGasInUse));

                ViewBag.CylinderDetails = reportview;



                //cylinder count Pstatewise
                //ReportViewer reportviewPstate = new ReportViewer();
                //reportview.ProcessingMode = ProcessingMode.Local;
                //reportview.SizeToReportContent = true;

              
                //ViewBag.reportviewPstate = reportviewPstate;

                //end


                ReportViewer reportview1 = new ReportViewer();
                reportview1.ProcessingMode = ProcessingMode.Local;
                reportview1.SizeToReportContent = true;

                List<usp_CylinderMasterGet_Result> CylinderList = new List<usp_CylinderMasterGet_Result>();



                CylinderList = InventoryEntities.usp_CylinderMasterGet(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview1.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderDetailLocation.rdlc";
                //locationname
                List<ReportParameter> parameters = new List<ReportParameter>();
                ReportParameter param = new ReportParameter();
                param.Name = "locationname";
                //parameters.Add(param);
                reportview1.LocalReport.SetParameters(param);
                reportview1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));
                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }



        public ActionResult ReportCylinderDetails()
        {
            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
                USP_GetUserDetails_Result logindetails;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];

                //for cylinder count location wise
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;



                List<usp_CylinderMasterGet_Result> CylinderList= new List<usp_CylinderMasterGet_Result>();

                CylinderList = InventoryEntities.usp_CylinderMasterGet(logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderDetails.rdlc";
                reportview.ShowToolBar = true;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));

                
                ViewBag.CylinderDetails = reportview;

                
                
                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ReportCylinderRecieveDeliver()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ReportCylinderRecieveDeliver(FormCollection frm)
        {
            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
               // USP_GetUserDetails_Result logindetails;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                CylinderRecieveDeliver cylinderRecieveDeliver = new CylinderRecieveDeliver();
                TryUpdateModel(cylinderRecieveDeliver);
                //for cylinder count location wise
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;



                List<usp_TransactionDetails_Result> CylinderList = new List<usp_TransactionDetails_Result>();

                CylinderList = InventoryEntities.usp_TransactionDetails(cylinderRecieveDeliver.StartDate, cylinderRecieveDeliver.EndDate, logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TransactionDetails.rdlc";
                reportview.ShowToolBar = true;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));


                ViewBag.CylinderDetails = reportview;



                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }



        [HttpGet]
        public ActionResult ReportCylinderRefill()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ReportCylinderRefill(FormCollection frm)
        {
            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
              //  USP_GetUserDetails_Result logindetails;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                CylinderRecieveDeliver cylinderRecieveDeliver = new CylinderRecieveDeliver();
                TryUpdateModel(cylinderRecieveDeliver);
                //for cylinder count location wise
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;



                List<usp_TransactionDetailsRefill_Result> CylinderList = new List<usp_TransactionDetailsRefill_Result>();

                CylinderList = InventoryEntities.usp_TransactionDetailsRefill(cylinderRecieveDeliver.StartDate, cylinderRecieveDeliver.EndDate, logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\TransactionDetailsRefill.rdlc";
                reportview.ShowToolBar = true;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));


                ViewBag.CylinderDetails = reportview;



                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }



        [HttpGet]
        public ActionResult ReportCylinderAgeAnalysis()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ReportCylinderAgeAnalysis(FormCollection frm)
        {
            try
            {
                
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
          //      USP_GetUserDetails_Result logindetails;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                CylinderRecieveDeliver cylinderRecieveDeliver = new CylinderRecieveDeliver();
                TryUpdateModel(cylinderRecieveDeliver);
                //for cylinder count location wise
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;



                List<usp_CylinderAgeAnalysisReport_Result> CylinderList = new List<usp_CylinderAgeAnalysisReport_Result>();

                CylinderList = InventoryEntities.usp_CylinderAgeAnalysisReport(cylinderRecieveDeliver.StartDate, cylinderRecieveDeliver.EndDate, logindetails.Branch_Id, logindetails.Company_Id).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderAgeAnalysisReport.rdlc";
                reportview.ShowToolBar = true;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));


                ViewBag.CylinderDetails = reportview;



                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult ReportCylinderExpiry()
        {
            try
            {

                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
             //   USP_GetUserDetails_Result logindetails;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
               
                ReportViewer reportview = new ReportViewer();
                reportview.ProcessingMode = ProcessingMode.Local;
                reportview.SizeToReportContent = true;

               
 


                List<usp_CylinderExpiryReport_Result> CylinderList = new List<usp_CylinderExpiryReport_Result>();

                CylinderList = InventoryEntities.usp_CylinderExpiryReport(logindetails.Company_Id, logindetails.Branch_Id ).ToList();
                reportview.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"\Reports\CylinderExpiryReport.rdlc";
                reportview.ShowToolBar = true;
                reportview.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", CylinderList));


                ViewBag.CylinderDetails = reportview;



                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        

        public void Drillthrough(object sender)
        {

        }

    }
}
