using System.Web.Mvc;
using Project.WebUI.Filters;

namespace Project.WebUI.Controllers.Home
{
    [MyAuthorize]
    public partial class HomeController
    {
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}