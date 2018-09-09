using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;

namespace IndoGhana.Areas.Masters.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Masters/Vehicle
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();  
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
        public ActionResult GetVehicleList()
        {

            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
                USP_GetUserDetails_Result logindetails=new USP_GetUserDetails_Result();
                if (Session["logindetails"] != null)
                {
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                 }
                List<usp_VehicleMasterGet_Result> users = new List<usp_VehicleMasterGet_Result>();
                users = InventoryEntities.usp_VehicleMasterGet(logindetails.Company_Id, logindetails.Branch_Id).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult CreateVehicle()
        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index", "UserLogin", new { area = "Login" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateVehicle(FormCollection frm)
        {

            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
                USP_GetUserDetails_Result logindetails = new USP_GetUserDetails_Result();
                if (Session["logindetails"] != null)
                {
                    logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                }
                usp_VehicleMasterGetByID_Result vehicle = new usp_VehicleMasterGetByID_Result();
                TryUpdateModel(vehicle);
                string result=Convert.ToString( InventoryEntities.usp_VehicleMasterInsertUpate(0, vehicle.VehicleNumber, logindetails.Company_Id, 
                    logindetails.Branch_Id,DateTime.Now, logindetails.USer_Id,0,null,vehicle.status).SingleOrDefault());
                if (result == "Duplicate Vehicle")
                {
                    ModelState.AddModelError("Error", "Vehicle already exists");
                    return View();
                }
                else


                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }

            
        }

        [HttpGet]
        public ActionResult EditVehicle(int VehicleID)
        {
            try {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
                USP_GetUserDetails_Result logindetails = new USP_GetUserDetails_Result();
                if (Session["logindetails"] != null)
                {
                    logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                }
                usp_VehicleMasterGetByID_Result vehicle = new usp_VehicleMasterGetByID_Result();
                vehicle=InventoryEntities.usp_VehicleMasterGetByID(logindetails.Company_Id, logindetails.Branch_Id, VehicleID).SingleOrDefault();
                return View(vehicle);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public ActionResult EditVehicle(FormCollection frm)
        {

            try
            {
                if (Session["logindetails"] == null)
                {
                    Session.Abandon();
                    return RedirectToAction("Index", "UserLogin", new { area = "Login" });
                }
                USP_GetUserDetails_Result logindetails = new USP_GetUserDetails_Result();
                if (Session["logindetails"] != null)
                {
                    logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                }

                usp_VehicleMasterGetByID_Result vehicle = new usp_VehicleMasterGetByID_Result();
                TryUpdateModel(vehicle);
                string result = Convert.ToString(InventoryEntities.usp_VehicleMasterInsertUpate(vehicle.VehicleID, vehicle.VehicleNumber, logindetails.Company_Id,
                    logindetails.Branch_Id, DateTime.Now, 0, logindetails.USer_Id, DateTime.Now, vehicle.status));
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }

                
        }
    }
}