using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;

namespace IndoGhana.Areas.Masters.Controllers
{
    public class VendorController : Controller
    {
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        // GET: Masters/Vendor
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            return View();
        }
        public ActionResult VendorList()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }

            try
            {

                List<usp_VendorMasterGet_Result> VendorList = InventoryEntities.usp_VendorMasterGet().ToList();
                return Json(VendorList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return View("Index"); }
        }

        public ActionResult VendorSiteList(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }

            try
            {

                List<usp_VendorBranchMasterGet_Result> siteList = InventoryEntities.usp_VendorBranchMasterGet(id).ToList();
                return Json(siteList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return View("Index"); }
        }


        public ActionResult CreateVendorDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public ActionResult CreateVendorDetails(FormCollection frm)
        {
            try
            {

                usp_VendorMasterGetbyID_Result Vendordetails = new usp_VendorMasterGetbyID_Result();
                TryUpdateModel(Vendordetails);
                USP_GetUserDetails_Result logindetails;
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                // }
                string result = (string)InventoryEntities.usp_VendorMasterInsertUpdate(0, Vendordetails.VendorName, Vendordetails.VendorAddress, 
                    Vendordetails.ContactPersonName, Vendordetails.ContactNumber
                    , Vendordetails.EmailID, logindetails.Company_Id, logindetails.Branch_Id,  logindetails.USer_Id, 0, DateTime.Now, Vendordetails.status
                    ).FirstOrDefault();
                if (result == "Duplicate Vendor")
                {

                    ModelState.AddModelError("Error", "Vendor already exists");

                    return View();
                }

                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }



        public ActionResult CreateVendorBranchDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            FillViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult CreateVendorBranchDetails(FormCollection frm)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            try
            {
                USP_GetUserDetails_Result logindetails;
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                usp_VendorBranchMasterGetbyID_Result vendorBranch= new usp_VendorBranchMasterGetbyID_Result();
                TryUpdateModel(vendorBranch);

                string result = (string)InventoryEntities.usp_VendorMasterBranchInsertUpdate(vendorBranch.VendorID, vendorBranch.VendorBranchID, vendorBranch.VendorBranchName, vendorBranch.VendorBranchAddress, 
                    vendorBranch.ContactPersonName, vendorBranch.ContactNumber
                , vendorBranch.EmailID, 0, logindetails.USer_Id, DateTime.Now, vendorBranch.status).FirstOrDefault();
                if (result == "Duplicate Vendor Branch")
                {

                    ModelState.AddModelError("Error", "Owner Branch already exists");
                    FillViewBag();
                    return View();
                }

                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditVendorDetails(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            // FillViewBag();
            usp_VendorMasterGetbyID_Result Vendordetails = InventoryEntities.usp_VendorMasterGetbyID(id).FirstOrDefault();
            return View(Vendordetails);
        }

        [HttpPost]
        public ActionResult EditVendorDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            try
            {
                USP_GetUserDetails_Result logindetails;
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                usp_VendorMasterGetbyID_Result Vendordetails = new usp_VendorMasterGetbyID_Result();
                TryUpdateModel(Vendordetails);

                string result = (string)InventoryEntities.usp_VendorMasterInsertUpdate(Vendordetails.VendorID, Vendordetails.VendorName, Vendordetails.VendorAddress, Vendordetails.ContactPersonName, Vendordetails.ContactNumber
                , Vendordetails.EmailID, logindetails.Branch_Id, logindetails.Company_Id, 0, logindetails.USer_Id, DateTime.Now, Vendordetails.status).FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditVendorSiteDetails(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            FillViewBag();
            usp_VendorBranchMasterGetbyID_Result VendorSitedetails = InventoryEntities.usp_VendorBranchMasterGetbyID(id).FirstOrDefault();
            return View(VendorSitedetails);
        }

        [HttpPost]
        public ActionResult EditVendorSiteDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            try
            {
                USP_GetUserDetails_Result logindetails;
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                usp_VendorBranchMasterGetbyID_Result Vendordetails = new usp_VendorBranchMasterGetbyID_Result();
                TryUpdateModel(Vendordetails);

                string result = (string)InventoryEntities.usp_VendorMasterBranchInsertUpdate(Vendordetails.VendorID, Vendordetails.VendorBranchID, 
                    Vendordetails.VendorBranchName, Vendordetails.VendorBranchAddress, Vendordetails.ContactPersonName, Vendordetails.ContactNumber
                , Vendordetails.EmailID, logindetails.USer_Id, logindetails.USer_Id, DateTime.Now, Vendordetails.status).FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }


        private void FillViewBag()
        {
            ViewBag.VendorID = new SelectList(InventoryEntities.usp_VendorListGet(), "VendorID", "VendorName");
        }

    }
}