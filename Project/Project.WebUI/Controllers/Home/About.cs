using System.Web.Mvc;
using Project.WebUI.Filters;

namespace Project.WebUI.Controllers.Home
{
    [MyAuthorize]
    public partial  class HomeController
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
    }
}