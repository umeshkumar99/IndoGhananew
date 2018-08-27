using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;

namespace IndoGhana.Areas.CylinderTransaction.Controllers
{
    public class CylinderFillController : Controller
    {
        // GET: CylinderTransaction/CylinderFill
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        USP_GetUserDetails_Result logindetails;
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection frm)
        {
            try
            {
                
                string result = string.Empty;
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                if (frm["barcodevalues"].ToString() != "")
                {
                    result=Convert.ToString( InventoryEntities.usp_tblCylinderFillInsert(frm["barcodevalues"].ToString(), logindetails.USer_Id, logindetails.Branch_Id, logindetails.Company_Id));
                }
                return View();
            }
        
        
            catch {
                return View();
            }
        }
        public ActionResult checkCyliderDetailsbyBarCode(string barcode)
        {
            try
            {
                usp_CylinderMasterGetbyBarCode_Result cylinderdetails = InventoryEntities.usp_CylinderMasterGetbyBarCode(barcode).FirstOrDefault();
                return Json(cylinderdetails, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult VehicleUserMappingDetails()
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

        public ActionResult VehicleUserMappingDetails(FormCollection frm)
        {
            try { 
                    if (Session["logindetails"] == null)
                    {
                        Session.Abandon();
                        return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                    }
                VehicleUserMapping vehicleUserMapping = new VehicleUserMapping();
                TryUpdateModel(vehicleUserMapping);
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                string result=Convert.ToString( InventoryEntities.usp_VehicleUserMappingInsert(vehicleUserMapping.VehicleID, vehicleUserMapping.UserD, logindetails.Branch_Id, logindetails.Company_Id, logindetails.USer_Id).FirstOrDefault());
                if (result == "Vehicle Unavailable")
                {
                    ModelState.AddModelError("Error", "Vehicle already assigned to the user for today");
                    FillViewBag();
                    return View();
                }
                else
                  return  RedirectToAction("VehicleUserMappingDetails");

                
            }
            catch (Exception ex)
            {
                return View();
            }
        }
            public ActionResult VehicleUserMappingDetailsGet()
        {
            try {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
               

                USP_GetUserDetails_Result logindetails;

                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                List<usp_VehicleUserMappingDetails_Result> usp_VehicleUserMappingDetails = InventoryEntities.usp_VehicleUserMappingDetails(logindetails.Company_Id, logindetails.Branch_Id).ToList();
                return Json(usp_VehicleUserMappingDetails, JsonRequestBehavior.AllowGet);
          


            }

            
            catch(Exception ex)
            {
                return View();

            }
        }

        private void FillViewBag()
        {
            logindetails = (USP_GetUserDetails_Result)Session["logindetails"];

            ViewBag.VehicleID = new SelectList(InventoryEntities.usp_VehicleMasterListGet(logindetails.Company_Id, logindetails.Branch_Id), "VehicleID", "VehicleNumber");
            ViewBag.UserD = new SelectList(InventoryEntities.usp_tblUserMasterListGet(logindetails.Company_Id, logindetails.Branch_Id), "User_Id", "User_Name");
        }

        }
}