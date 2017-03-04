using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.WebUI.Models;

namespace Project.WebUI.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationManager>();
            if (string.IsNullOrEmpty(httpContext.User.Identity.Name))
            {
                return false;
            }
            var user = userManager.FindByName(httpContext.User.Identity.Name);
            if ((user != null) && (user.Banned == true))
            {
                return false;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}