using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CylnderEntities;
using System.Net;

namespace IndoGhana.Areas.Login.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: Login/UserLogin
        IndoGhanaEntities InventoryEntities = new IndoGhanaEntities();
        string IPAddress;
        public ActionResult Index()
        {

            
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginDetails login)
        {
            try
            {
                TryUpdateModel(login);
                string username = Request["username"];
                string password = Request["password"];
                //USP_GetUserDetails_Result logindetails= InventoryEntities.USP_GetUserDetails(login.UserName, login.Password, login.Phone).FirstOrDefault();
                USP_GetUserDetails_Result logindetails = InventoryEntities.USP_GetUserDetails(username, password, "").FirstOrDefault();
                // return View();
                if (logindetails.USer_Id == 0)
                {
                    ModelState.AddModelError("Error", "Invalid User name or Password. Please try again.");
                    return View();
                }
                Session["logindetails"] = logindetails;
                return RedirectToAction("Index", "CylinderDetails", new { area = "CylinderDetails" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult ListUsers()

        {
            if (Session["logindetails"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult ListUsersDetails()
        {
            List<usp_tblUserMasterGet_Result> users = new List<usp_tblUserMasterGet_Result>();
            users = InventoryEntities.usp_tblUserMasterGet().ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public ActionResult ListUsers(FormCollection frm)
        {
            return View();
        }

        public ActionResult EditUser(int User_Id)
        {
            FillViewBag();
            usp_tblUserMasterGetByID_Result userdetails= InventoryEntities.usp_tblUserMasterGetByID(User_Id).FirstOrDefault();
            return View(userdetails);
        }
        [HttpPost]
        public ActionResult EditUser(FormCollection frm)
        {
            usp_tblUserMasterGetByID_Result userdetails = new usp_tblUserMasterGetByID_Result();
            TryUpdateModel(userdetails);
            USP_GetUserDetails_Result logindetails;
            //if (Session["logindetails"] != null)
            //{
            logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
            // }
            string result = (string)InventoryEntities.usp_tblUserMasterInsertUpdate(userdetails.User_Id, userdetails.User_Name, userdetails.Address, userdetails.Email,
                          userdetails.Contact_Number, userdetails.IMIE1, userdetails.IMIE2, userdetails.Login_Id, userdetails.Password, userdetails.Group_Id, logindetails.Company_Id,
                          DateTime.Now, userdetails.UserStatus, logindetails.USer_Id, userdetails.Branch_Id, 0).FirstOrDefault();
            return RedirectToAction("ListUsers");
        }
        public ActionResult CreateUserDetails()
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
        public ActionResult CreateUserDetails(FormCollection frm)
        {
            try
            {
                usp_tblUserMasterGetByID_Result userdetails = new usp_tblUserMasterGetByID_Result();
                TryUpdateModel(userdetails);
                USP_GetUserDetails_Result logindetails;
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                // }
                string result = (string)InventoryEntities.usp_tblUserMasterInsertUpdate(0, userdetails.User_Name, userdetails.Address, userdetails.Email,
                              userdetails.Contact_Number, userdetails.IMIE1, userdetails.IMIE2, userdetails.Login_Id, userdetails.Password, userdetails.Group_Id, logindetails.Company_Id,
                              DateTime.Now, userdetails.UserStatus, logindetails.USer_Id, userdetails.Branch_Id, 0).FirstOrDefault();
              
                if (result == "Duplicate user")
                {
                    FillViewBag();
                    ModelState.AddModelError("Error", "User already exists");
                  
                    return View();
                }
               
                    else
                                    return RedirectToAction("ListUsers");
            }
            catch(Exception ex)
            {
                return RedirectToAction("ListUsers");
            }
        }
        public ActionResult Logout()
        {
            USP_GetUserDetails_Result logindetails;
            try
            {


                IPAddress = GetIPAddress();
                //if (Session["logindetails"] != null)
                //{
                logindetails = (USP_GetUserDetails_Result)Session["logindetails"];
                // }

                InventoryEntities.usp_UpdateLogoutTime(logindetails.logid, IPAddress);
                Session.Abandon();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        private void FillViewBag()
        {
            ViewBag.Group_Id = new SelectList(InventoryEntities.usp_tblGroupGet(), "Group_Id", "Group_Name");
            ViewBag.Branch_Id = new SelectList(InventoryEntities.usp_tblCompanyBranchMasterListGet(), "BranchId", "BranchName");
            }
     

        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

    }
    }