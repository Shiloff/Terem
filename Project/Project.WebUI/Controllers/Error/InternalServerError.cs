using System.Net;
using System.Web.Mvc;

namespace Project.WebUI.Controllers.Error
{
    public partial class ErrorController
    {
        public ActionResult InternalServerError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
	}
}