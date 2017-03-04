using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.WebUI.Filters;
namespace Project.WebUI.Controllers
{
    [MyAuthorize(Roles="Admin")]
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }
	}
}