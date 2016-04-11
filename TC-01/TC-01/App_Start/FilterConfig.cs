using System.Web;
using System.Web.Mvc;

namespace TC_01
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //Forces controllers to use SSL
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
