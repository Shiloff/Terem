using System.Web;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Project.WebUI.Models;

namespace Project.WebUI.Infrastructure.ApplicationUser
{
    public class OwinApplicationManager : IApplicationManager
    {
        private ApplicationManager _manager;
        private ApplicationUserEntity _user;


        public OwinApplicationManager()
        {
            _manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationManager>();
        }

        public ApplicationUserEntity CurrentUser
        {
            get
            {
                if (_user?.UserName == null)
                {
                    _user = _manager.FindById(HttpContext.Current.User.Identity.GetUserId());
                }
                return _user;
            }
        }

        public ApplicationManager UserManager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public void Dispose()
        {
            _manager.Dispose();
            _user = null;
            _manager = null;
        }
    }
}