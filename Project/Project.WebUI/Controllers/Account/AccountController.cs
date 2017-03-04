using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Business.DataAccess.Public.Entities.Identity;
using Business.DataAccess.Public.Repository.Specific;
using Business.DataAccess.Public.Services.Profile;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Project.WebUI.Filters;
using Project.WebUI.Infrastructure.ApplicationUser;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Account
{
    [MyAuthorize]

    public partial class AccountController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IApplicationManager _applicationManager;
        private readonly IProfileService _profileService;

        public AccountController(IProfileRepository profileRepository,
            IApplicationManager applicationManager, 
            IProfileService profileService)
        {
            _profileRepository = profileRepository;
            _applicationManager = applicationManager;
            _profileService = profileService;
            //UserManager = applicationManager;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //Uri locationHeader = new Uri(Url.RouteUrl("GetUserById", new { id = "6b26a2e8-4c5e-4ca6-8917-85c31d3f6d41" }));

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _applicationManager.UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    user.LastLoginTime = DateTime.Now;
                    await _applicationManager.UserManager.UpdateAsync(user);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUserEntity() { UserName = model.UserName, RegistrationDate = DateTime.Now, LastLoginTime = DateTime.Now, Email = model.Email };
                var result = await _applicationManager.UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _applicationManager.UserManager.AddToRoleAsync(user.Id, "user");
                    await SignInAsync(user, isPersistent: false);
                    var newProfile = new Business.DataAccess.Public.Entities.Profile { UserId = user.Id, New = true };
                    _profileRepository.NewProfile(newProfile);
                    user.ProfileId = newProfile.ProfileId;
                    _profileService.AddProfileAction(new ProfileAction
                    {
                        ProfileId = newProfile.ProfileId,
                        Date = DateTime.Now,
                        ProfileActionTypeId = 2,
                        Text = " Зарегистрировался!",
                        ProfileWhoId = newProfile.ProfileId
                    });
                    await _applicationManager.UserManager.UpdateAsync(user);

                    SendEmailComfirmation(user.Id);
                    
                    //string code = this.UserManager.GenerateEmailConfirmationToken(user.Id);                    
                    //var callbackUrl = new Uri(Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme));
                    //this.UserManager.SendEmail(user.Id, "Подтверждение Email", "Пожалуйста подтвердите Email пройдя по <a href=\"" + callbackUrl + "\">ссылке</a>");

                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
           return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._applicationManager.UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    try
                    {
                        string resetToken = await this._applicationManager.UserManager.GeneratePasswordResetTokenAsync(user.Id);
                        var callbackUrl = new Uri(Url.Action("ChangePassword", "Account", new { userId = user.Id, token = resetToken }, protocol: Request.Url.Scheme));
                        this._applicationManager.UserManager.SendEmail(user.Id, "Сброс пароля", "Измените ваш пароль, пройдя по <a href=\"" + callbackUrl + "\">ссылке</a>");
                        return View("ResetPasswordSended");
                    }
                    catch(Exception e)
                    {
                        Project.WebUI.Infrastructure.Core.Log.Logger.Error("{0}" + System.Environment.NewLine + "{1}", e.Message, e.StackTrace);
                        return View("ResetPasswordError");
                    }
                }
            }
            return View("ResetPasswordError");
        }
        [AllowAnonymous]
        public ActionResult ChangePassword(string userId,string token)
        {
            ChangePasswordViewModel result = new ChangePasswordViewModel() { UserId = userId, Token = token };
            return View(result);
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await this._applicationManager.UserManager.ResetPasswordAsync(model.UserId, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    return View("ChangePasswordOk");
                }
                else
                {
                    AddErrors(result);                    
                }
            }
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return View("ConfirmEmailError");
            }

            IdentityResult result = await this._applicationManager.UserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return View("ConfirmEmailOk");
            }
            else
            {
                return View("ConfirmEmailError");
            }
        }
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await _applicationManager.UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
        public async Task<ActionResult> ChangeEmail()
        {
            ChangeUserEmailViewModel result = new ChangeUserEmailViewModel();
            var user = await _applicationManager.UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                result.Email = user.Email;
                result.EmailConfirmed = user.EmailConfirmed;
            }
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeUserEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _applicationManager.UserManager.FindByIdAsync(User.Identity.GetUserId());

                IdentityResult result = await this._applicationManager.UserManager.SetEmailAsync(user.Id, model.Email);
                if (result.Succeeded)
                {
                   
                    SendEmailComfirmation();
                    if (ModelState.IsValid)
                    {
                        TempData["toastrMessage"] = String.Format("Email успешно изменен, на почту выслан запрос на подтверждение");
                        TempData["toastrType"] = "success";
                    }
                    return View();
                }
                else {
                    AddErrors(result);
                }
            }
            return View();
        }
        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _applicationManager.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["toastrMessage"] = String.Format("Пароль успешно изменен");
                        TempData["toastrType"] = "success";
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await _applicationManager.UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
       
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await _applicationManager.UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await _applicationManager.UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUserEntity() { UserName = model.UserName };
                var result = await _applicationManager.UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _applicationManager.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Profile");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //[ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = _applicationManager.UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }
        public void SendEmailComfirmation(string userId = null)
        {
            try
            {
                var UserId = userId ?? User.Identity.GetUserId();
                string code = this._applicationManager.UserManager.GenerateEmailConfirmationToken(UserId);
                var callbackUrl = new Uri(Url.Action("ConfirmEmail", "Account", new { userId = UserId, code = code }, protocol: Request.Url.Scheme));
                this._applicationManager.UserManager.SendEmail(UserId, "Подтверждение Email", "Пожалуйста подтвердите Email пройдя по <a href=\"" + callbackUrl + "\">ссылке</a>");
            }
            catch (Exception e)
            {
                Project.WebUI.Infrastructure.Core.Log.Logger.Error("{0}" + System.Environment.NewLine + "{1}", e.Message, e.StackTrace);
                ModelState.AddModelError("", "Ошибка при отправке письма на почтовый ящик");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _applicationManager.UserManager != null)
            {
                _applicationManager.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUserEntity user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _applicationManager.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = _applicationManager.UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Profile");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}