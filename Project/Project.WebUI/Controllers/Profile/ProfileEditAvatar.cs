using System;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Business.DataAccess.Public.Repository.Specific;
using Project.WebUI.Models;
using Project.WebUI.Services;

namespace Project.WebUI.Controllers.Profile
{

    public partial class ProfileController : Controller
    {
        public ActionResult ProfileEditAvatar()
        {
            var result = new ProfileEditResult
            {
                Profile = _profileService.GetProfileWithDetails((long) _applicationManager.CurrentUser.ProfileId),
            };
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileEditAvatar(ProfileEditResult newProfile, double? cropx = 0, double? cropy = 0, double? croph = 0, double? cropw = 0)
        {
            if( (ModelState.IsValid))
            {
                if (newProfile.inputImage != null)
                {
                    newProfile.inputImage = newProfile.inputImage ?? Request.Files["inputImage"];
                    if (newProfile.inputImage != null)
                    {
                        var extension = Path.GetExtension(newProfile.inputImage.FileName);
                        if (!string.IsNullOrWhiteSpace(extension))
                        {
                            if (newProfile.Profile.ImageLink!=null)
                            {
                                PreviewCreator.RemoveImage(System.Web.Hosting.HostingEnvironment.MapPath(newProfile.Profile.ImageLink));
                            }
                            if (newProfile.Profile.ImageAvatarLink != null)
                            {
                                PreviewCreator.RemoveImage(System.Web.Hosting.HostingEnvironment.MapPath(newProfile.Profile.ImageAvatarLink));
                            }
                            if (newProfile.Profile.ImageAvatarBigLink != null)
                            {
                                PreviewCreator.RemoveImage(System.Web.Hosting.HostingEnvironment.MapPath(newProfile.Profile.ImageAvatarBigLink));
                            }
                            
                            newProfile.Profile.ImageType = ("image/" + extension).Replace(".", "");
                            newProfile.Profile.ImageLink = PreviewCreator.SaveImage(newProfile.inputImage.InputStream);
                            
                            newProfile.Profile.ImageAvatarType = ("image/" + ".jpg").Replace(".", "");
                            newProfile.Profile.ImageAvatarLink =PreviewCreator.SaveImageAndResize(newProfile.inputImage.InputStream, (int)cropx, (int)cropy, (int)cropw, (int)croph, new Size(48, 48));
                            
                            newProfile.Profile.ImageAvatarBigType = ("image/" + ".jpg").Replace(".", "");
                            newProfile.Profile.ImageAvatarBigLink = PreviewCreator.SaveImageAndResize(newProfile.inputImage.InputStream, (int)cropx, (int)cropy, (int)cropw, (int)croph, new Size(550, 550));
                            
                           
                        }
                    }
                }
                else
                {
                    if ((newProfile.Profile.ImageLink != null) && (cropx != 0) && (cropy != 0) && (cropw != 0) && (croph != 0))
                    {
                        using (var fileStream = new FileStream(Server.MapPath(newProfile.Profile.ImageLink), FileMode.Open, FileAccess.Read))
                        {
                            if (newProfile.Profile.ImageAvatarLink != null)
                            {
                                PreviewCreator.RemoveImage(System.Web.Hosting.HostingEnvironment.MapPath(newProfile.Profile.ImageAvatarLink));
                            }
                            if (newProfile.Profile.ImageAvatarBigLink != null)
                            {
                                PreviewCreator.RemoveImage(System.Web.Hosting.HostingEnvironment.MapPath(newProfile.Profile.ImageAvatarBigLink));
                            }
                            newProfile.Profile.ImageAvatarType = ("image/" + ".jpg").Replace(".", "");
                            newProfile.Profile.ImageAvatarLink = PreviewCreator.SaveImageAndResize(fileStream, (int)cropx, (int)cropy, (int)cropw, (int)croph, new Size(48, 48));
                            newProfile.Profile.ImageAvatarBigType = ("image/" + ".jpg").Replace(".", "");
                            newProfile.Profile.ImageAvatarBigLink = PreviewCreator.SaveImageAndResize(fileStream, (int)cropx, (int)cropy, (int)cropw, (int)croph, new Size(550, 550)); ;

                        }
                    }
                }


                newProfile.Profile.New = false;
                _profileService.UpdateProfile(newProfile.Profile, ProfileUpdateMode.Avatar);
                UpdateProfileMEssage();
            }
            return View(newProfile);
        }
    }
}