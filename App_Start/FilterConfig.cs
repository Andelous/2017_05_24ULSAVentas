using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
