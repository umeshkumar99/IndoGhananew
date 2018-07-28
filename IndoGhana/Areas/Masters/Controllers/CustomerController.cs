using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;

namespace IndoGhana.Areas.Masters.Controllers
{
    public class CustomerController : Controller
    {
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        // GET: Masters/Customer
        public ActionResult Index()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }
            return View();
        }
        public ActionResult CustomerList()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }

        try {

               List<usp_CustomerMasterGet_Result> customerList = InventoryEntities.usp_CustomerMasterGet().ToList();
                return Json(customerList, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            { return View("Index"); }
        }

        public ActionResult CustomerSiteList(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }

            try
            {

                List<usp_CustomerSiteMasterGetbyID_Result> siteList = InventoryEntities.usp_CustomerSiteMasterGetbyID(id).ToList();
                return Json(siteList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { return View("Index"); }
        }


       public ActionResult CreateCustomerDetails()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
           
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomerDetails(FormCollection frm)
        {
            try { 
            usp_CustomerMasterGetbyID_Result customerdetails = new usp_CustomerMasterGetbyID_Result();
            TryUpdateModel(customerdetails);
            USP_GetUserDetails_Result logindetails;
            //if (Session["logindetails"] != null)
            //{
            logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
            // }
            string result = (string)InventoryEntities.usp_CustomerMasterInsertUpdate(0, customerdetails.CustomerName, customerdetails.CustomerAddress, customerdetails.ContactPersonName, customerdetails.ContactNumber
                , customerdetails.Email, logindetails.Branch_Id, logindetails.Company_Id, logindetails.USer_Id, 0, DateTime.Now, customerdetails.status, customerdetails.IsOwner).FirstOrDefault();
            if (result == "Duplicate customer")
            {

                ModelState.AddModelError("Error", "Customer already exists");

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



        public ActionResult CreateCustomerSiteDetails()
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
        public ActionResult CreateCustomerSiteDetails(FormCollection frm)
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
                usp_CustomerSiteMasterGetbyID_Result customerdetails = new usp_CustomerSiteMasterGetbyID_Result();
                TryUpdateModel(customerdetails);

                string result = (string)InventoryEntities.usp_CustomerMasterSiteInsertUpdate(customerdetails.CustomerID, customerdetails.CustomerIDSiteID, customerdetails.SiteName, customerdetails.SiteAddress, customerdetails.ContactPersonName, customerdetails.ContactNumber
                , customerdetails.Email, logindetails.USer_Id, logindetails.USer_Id, DateTime.Now, customerdetails.status).FirstOrDefault();
                if (result == "Duplicate Customer Site")
                {

                    ModelState.AddModelError("Error", "Customer site already exists");
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

        public ActionResult EditCustomerDetails(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
           // FillViewBag();
            usp_CustomerMasterGetbyID_Result customerdetails = InventoryEntities.usp_CustomerMasterGetbyID(id).FirstOrDefault();
            return View(customerdetails);
        }

        [HttpPost]
        public ActionResult EditCustomerDetails()
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
                usp_CustomerMasterGetbyID_Result customerdetails = new usp_CustomerMasterGetbyID_Result();
                TryUpdateModel(customerdetails);
                
                string result = (string)InventoryEntities.usp_CustomerMasterInsertUpdate(customerdetails.CustomerID, customerdetails.CustomerName, customerdetails.CustomerAddress, customerdetails.ContactPersonName, customerdetails.ContactNumber
                , customerdetails.Email, logindetails.Branch_Id, logindetails.Company_Id, 0, logindetails.USer_Id, DateTime.Now, customerdetails.status, customerdetails.IsOwner).FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult EditCustomerSiteDetails(int id)
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
             FillViewBag();
            usp_CustomerSiteMasterGetbyIDDetails_Result customerSitedetails = InventoryEntities.usp_CustomerSiteMasterGetbyIDDetails(id).FirstOrDefault();
            return View(customerSitedetails);
        }

        [HttpPost]
        public ActionResult EditCustomerSiteDetails()
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
                usp_CustomerSiteMasterGetbyID_Result customerdetails = new usp_CustomerSiteMasterGetbyID_Result();
                TryUpdateModel(customerdetails);

                string result = (string)InventoryEntities.usp_CustomerMasterSiteInsertUpdate(customerdetails.CustomerID, customerdetails.CustomerIDSiteID,customerdetails.SiteName, customerdetails.SiteAddress, customerdetails.ContactPersonName, customerdetails.ContactNumber
                , customerdetails.Email, logindetails.USer_Id, logindetails.USer_Id,  DateTime.Now, customerdetails.status).FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }


        private void FillViewBag()
        {
            ViewBag.CustomerID = new SelectList(InventoryEntities.usp_CustomerListGet(), "CustomerID", "CustomerName");
        }

        }
    }