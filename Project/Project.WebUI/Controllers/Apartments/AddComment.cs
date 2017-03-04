
using System;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Apartments
{
    public partial class ApartmentsController : Controller
    {
        [HttpPost]
        public ActionResult AddComment(ApartmentComment apartmentComment)
        {
            InfoApartmentCommentResult result = new InfoApartmentCommentResult();
            if(ApartmentRepository.IsApartmentExists((long)apartmentComment.ApartmentId))
            {
                apartmentComment.Date = DateTime.Now;
                ApartmentRepository.AddApartmentComment(apartmentComment);
            }
            result.Me = ProfileRepository.GetProfile(user.ProfileId);
            result.Apartment = ApartmentRepository.GetApartment((long)apartmentComment.ApartmentId);
            result.Comment = ApartmentRepository.GetApartmentComment(apartmentComment.ApartmentCommentId);
            return PartialView("_InfoComment", result);
        }
	}
}