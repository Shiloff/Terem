using System.Net;
using System.Web.Mvc;

namespace Project.WebUI.Controllers.Error
{
    public partial class ErrorController
    {
        public ActionResult BadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View("NotFound");
        }       
       
	}
}