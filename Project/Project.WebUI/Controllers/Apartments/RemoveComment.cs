
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Controllers.Apartments
{
    public partial class ApartmentsController : Controller
    {

        public void RemoveComment(long? id)
        {
            ApartmentComment comment = ApartmentRepository.GetApartmentComment((long)id);
            Apartment apartment = ApartmentRepository.GetApartment((long)comment.ApartmentId);
            if (comment != null)
            {
                if ((comment.ProfileId == user.ProfileId) || (apartment.ProfileId == user.ProfileId))
                {
                    ApartmentRepository.RemoveApartmentComment((long)id);
                }
            }
        }
	}
}