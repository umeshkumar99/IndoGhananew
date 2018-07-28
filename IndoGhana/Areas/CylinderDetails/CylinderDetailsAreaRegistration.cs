using System.Web.Mvc;

namespace IndoGhana.Areas.CylinderDetails
{
    public class CylinderDetailsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CylinderDetails";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CylinderDetails_default",
                "CylinderDetails/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}