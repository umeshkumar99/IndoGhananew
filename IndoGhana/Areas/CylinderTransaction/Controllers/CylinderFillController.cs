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
            if(frm["barcodevalues"].ToString()!="")
            {

            }
            return View();
        }
        public ActionResult checkCyliderDetailsbyBarCode(string barcode)
        {
            usp_CylinderMasterGetbyBarCode_Result cylinderdetails = InventoryEntities.usp_CylinderMasterGetbyBarCode(barcode).FirstOrDefault();
            return Json(cylinderdetails, JsonRequestBehavior.AllowGet);
        }
    }
}