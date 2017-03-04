
using System.Web.Mvc;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Apartments
{
    public partial class ApartmentsController : Controller
    {

        public ActionResult RemoveCommentLike(long? ApartmentCommentId)
        {

            InfoApartmentCommentResult result = new InfoApartmentCommentResult();
            ApartmentRepository.RemoveApartmentCommentLike((long)ApartmentCommentId, (long)user.ProfileId);
            result.Comment = ApartmentRepository.GetApartmentComment((long)ApartmentCommentId);
            result.Apartment = ApartmentRepository.GetApartment((long)result.Comment.ApartmentId);
            result.Me = ProfileRepository.GetProfile(user.ProfileId);
            return PartialView("_InfoComment", result);
        }
	}
}