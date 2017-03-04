
using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Apartments
{
    public partial class ApartmentsController : Controller
    {
        IApartmentRepository ApartmentRepository;
        IProfileRepository ProfileRepository;
        ApplicationUserEntity user
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationManager>().FindById(HttpContext.User.Identity.GetUserId());
            }
        }
        //
        // GET: /Apartment/
        public ApartmentsController(IApartmentRepository apartmentRepository, IProfileRepository profileRepository)
        {
            ApartmentRepository = apartmentRepository;
            ProfileRepository = profileRepository;
        }
        public ActionResult Index(int page = 1)
        {
            int PageSize = 10;
            IndexApartmentsViewResult result = new IndexApartmentsViewResult();
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.CurrentPage = page;
            pagingInfo.ItemsPerPage = PageSize;
            pagingInfo.TotalItems = (int)ApartmentRepository.GetApartmentsCount(new GetApartmentsParams());
            result.PagingInfo = pagingInfo;
            result.Apartments = ApartmentRepository.GetApartments(new GetApartmentsParams()
            {
                Take = PageSize,
                Skip = (page - 1) * PageSize
            });
            return View(result);
        }
	}
}