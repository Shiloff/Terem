using System.Web.Http;
using Business.DataAccess.Public.Entities.Identity;

namespace Project.Controllers
{
    public class UsersController : ApiController
    {        
        public string GetAll()
        {
           
            return null;
        }
        public ApplicationUserEntity Get(int id)
        {
            ApplicationUserEntity result = new ApplicationUserEntity();

            return result;
        }
    }
}
