using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Project.WebUI.Models;
using Project.WebUI.Services;

namespace Project.WebUI.Controllers
{
    public partial class RentController : Controller
    {

        public ActionResult EditApartmentPhoto(long id = 0)
        {
            EditApartmentViewResult result = new EditApartmentViewResult();
            result.Apartment = ApartmentRepository.GetApartment(id);
            result.ApartmentOptions = ApartmentRepository.GetApartmentOptions();
            if (result.Apartment == null)
            {
                result.Apartment = new Apartment();
                result.Apartment.ApartmentOptions = new List<ApartmentOption>();
                result.Apartment.ApartmentPhotos = new List<ApartmentPhoto>();
            }
            result.ApartmentTypes = ApartmentRepository.ApartmentTypes.ToList();
            return View(result);
        }
       

        [HttpPost]
        public ActionResult EditApartmentPhoto(EditApartmentPhotoViewResult newApartment)
        {
            if (ModelState.IsValid)
            {
                if (newApartment.InputImages != null)
                { 
                    Apartment apartment = ApartmentRepository.GetApartment(newApartment.ApartmentId);
                    var Photos = ApartmentRepository.GetApartmentPhotos(apartment);
                    var HasDafaultPhoto = true;
                    if (Photos.Count==0)
                    {
                        HasDafaultPhoto = false;
                    }
                    foreach (var photo in newApartment.InputImages.Where(m => m != null))
                    {
                        int height, width, max;
                        int centerx, centery;
                        int offsetx, offsety;
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(photo.InputStream, true, true))
                        {
                            height = image.Height;
                            width = image.Width;
                            centerx = width / 2;
                            centery = height / 2;
                            if (height > width)
                            {
                                max = width;
                                offsetx = 0;
                                offsety = centery - max / 2;
                            }
                            else
                            {
                                max = height;
                                offsety = 0;
                                offsetx = centerx - max / 2;
                            }

                        }
                        ApartmentPhoto Photo = new ApartmentPhoto { ApartmentId = newApartment.ApartmentId, Links = new List<ApartmentPhotoLink>() };
                        Photo.Links.Add(new ApartmentPhotoLink { Link = PreviewCreator.SaveImage(photo.InputStream), TypeId = 1 });
                        Photo.Links.Add(new ApartmentPhotoLink { Link = PreviewCreator.SaveImageAndResize(photo.InputStream, (int)offsetx, (int)offsety, (int)max, (int)max, new System.Drawing.Size(550, 550)), TypeId = 2 });
                        Photo.Links.Add(new ApartmentPhotoLink { Link = PreviewCreator.SaveImageAndResize(photo.InputStream, (int)offsetx, (int)offsety, (int)max, (int)max, new System.Drawing.Size(100, 100)), TypeId = 3 });
                       
                        ApartmentRepository.AddApartmentPhoto(apartment, Photo);
                        if (!HasDafaultPhoto)
                        {
                            ApartmentRepository.SetApartmentDefaultPhoto(apartment, Photo);
                        }
                        HasDafaultPhoto = true;
                        TempData["toastrMessage"] = String.Format("Квартира изменена");
                        TempData["toastrType"] = "success";
                    }
                }
               
            }
            else
            {
            }

            return RedirectToAction("EditApartmentPhoto");
        }
        [HttpPost]
        public ActionResult RemovePhotoApartment(RemovePhotoApartmentData value)
        {
            var MyApartments = ApartmentRepository.GetMyApartments((int)user.ProfileId);
            var MyPhotos = MyApartments.SelectMany(m => m.ApartmentPhotos).ToList();
            var Photo = MyPhotos.Where(m => m.ApartmentPhotoId == value.ApartmentPhotoId).FirstOrDefault();
            if ((value.ApartmentPhotoId != null) && (Photo != null))
            {
                var a = MyApartments.Where(m => m.ApartmentPhotos.Where(k => k.ApartmentPhotoId == value.ApartmentPhotoId).Count() > 0).FirstOrDefault();
                
                ApartmentRepository.RemoveApartmentPhoto(value.ApartmentPhotoId);
                var apartment = ApartmentRepository.GetApartment(a.ApartmentId);
                var apartmentPhotos = ApartmentRepository.GetApartmentPhotos(apartment);
                if ((apartment.DefaultPhotoId == null)&&(apartmentPhotos.Count!=0))
                {
                    ApartmentRepository.SetApartmentDefaultPhoto(apartment, apartmentPhotos[0]);
                }
            }
            return RedirectToAction("EditApartmentPhoto");
        }
        [HttpPost]
        public void SetApartmentDefaultPhoto(SetApartmentDefaultPhotoData value)
        {
            var MyApartments = ApartmentRepository.GetMyApartments((int)user.ProfileId);
            var MyPhotos = MyApartments.SelectMany(m => m.ApartmentPhotos).ToList();
            if ((value.PhotoId != null) && (MyPhotos.Where(m => m.ApartmentPhotoId == value.PhotoId).FirstOrDefault() != null))
            {
                var apartment = MyApartments.Where(m => m.ApartmentId == value.ApartmentId).FirstOrDefault();
                var photo = apartment.ApartmentPhotos.Where(m => m.ApartmentPhotoId == value.PhotoId).FirstOrDefault();
                ApartmentRepository.SetApartmentDefaultPhoto(apartment, photo);
            }
        }
	}
}