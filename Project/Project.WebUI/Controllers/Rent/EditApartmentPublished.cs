using System;
using System.Linq;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {
        public ActionResult EditApartmentSetPublished(long id = 0)
        {
            var myAparments = _apartmentRepository.GetMyApartments((int) user.ProfileId);
            if (myAparments.Select(m => m.ApartmentId).Contains(id))
            {
                _apartmentRepository.SetPublished(id);
                _profileService.AddProfileAction(new ProfileAction
                {
                    ProfileId = user.ProfileId,
                    Date = DateTime.Now,
                    ProfileActionTypeId = 3,
                    ApartmentId = id,
                    Text = "Опубликовал квартиру:",
                    ProfileWhoId = user.ProfileId
                });
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditApartmentRemovePublished(long id = 0)
        {
            var myAparments = _apartmentRepository.GetMyApartments((long) user.ProfileId);
            if (myAparments.Select(m => m.ApartmentId).Contains(id))
            {
                _apartmentRepository.RemovePublished(id);
                _profileService.AddProfileAction(new ProfileAction
                {
                    ProfileId = user.ProfileId,
                    Date = DateTime.Now,
                    ProfileActionTypeId = 4,
                    ApartmentId = id,
                    Text = "Квартира больше не показывается в списке активных",
                    ProfileWhoId = user.ProfileId
                });
            }
            return RedirectToAction("Index");
        }
    }
}