using System.Web.Mvc;

namespace IndoGhana.Areas.CylinderTransaction
{
    public class CylinderTransactionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CylinderTransaction";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CylinderTransaction_default",
                "CylinderTransaction/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}