using System.Web;
using System.Web.Mvc;
using Project.WebUI.Filters;
namespace Project.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new DiagnosticAttribute());
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
