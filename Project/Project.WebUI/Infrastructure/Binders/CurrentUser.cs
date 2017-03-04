using System.Web.Mvc;
using System.Web;
using Project.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace Project.WebUI.Infrastructure.Binders
{
    public class CurrentUserBinder : IModelBinder
    {
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //CurrentApplicationUser result;
            //ApplicationManager UserManager = controllerContext.RequestContext.HttpContext.GetOwinContext().GetUserManager<UserManager>();
            //var userId = controllerContext.RequestContext.HttpContext.User.Identity.GetUserId();
            //result = (CurrentApplicationUser)UserManager.GetCurrentUser(userId);
            //return result;
            return null;
        }
    }
}