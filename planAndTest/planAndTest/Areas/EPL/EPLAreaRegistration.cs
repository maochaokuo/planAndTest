using System.Web.Mvc;

namespace planAndTest.Areas.EPL
{
    public class EPLAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EPL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EPL_default",
                "EPL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}