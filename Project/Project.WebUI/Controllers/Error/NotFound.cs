using System.Net;
using System.Web.Mvc;

namespace Project.WebUI.Controllers.Error
{
    public partial class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;            
            return View();            
        }      
       
	}
}