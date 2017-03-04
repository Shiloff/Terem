using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using Business.DataAccess.Public.Entities;

namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {

        public ActionResult EditApartmentSetPublished(long id = 0)
        {
            List<Apartment> myAparments = ApartmentRepository.GetMyApartments((int)user.ProfileId);
            if (myAparments.Select(m => m.ApartmentId).Contains(id))
            {
                ApartmentRepository.SetPublished(id);
                ProfileRepository.AddProfileAction(new ProfileAction
                {
                    ProfileId = user.ProfileId,
                    Date = DateTime.Now,
                    ProfileActionTypeId = 3,
                    ApartmentId=id,
                    Text = "Опубликовал квартиру:",
                    ProfileWhoId = user.ProfileId
                });
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditApartmentRemovePublished(long id = 0)
        {
            List<Apartment> myAparments = ApartmentRepository.GetMyApartments((long)user.ProfileId);
            if (myAparments.Select(m => m.ApartmentId).Contains(id))
            {
                ApartmentRepository.RemovePublished(id);
                ProfileRepository.AddProfileAction(new ProfileAction
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