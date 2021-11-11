using System.Web.Mvc;

namespace WebStore.Areas.PointOfSale
{
    public class PointOfSaleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PointOfSale";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PointOfSale_default",
                "PointOfSale/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}