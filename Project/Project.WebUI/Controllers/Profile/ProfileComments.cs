using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Business.DataAccess.Public.Entities;
using Project.WebUI.Models;

namespace Project.WebUI.Controllers.Profile
{
    public partial class ProfileController : Controller
    {
        public ActionResult Actions(long? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var result = new ProfileActionsResult
            {
                Actions = _profileService.GetProfileActions((long) id),
                Profile = _profileService.GetShortProfile((long)_applicationManager.CurrentUser.ProfileId),
            };

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult AddAction(ProfileAction value)
        {
            var result = new AddActionResult();

            if ((value != null) && (_applicationManager.CurrentUser.ProfileId == value.ProfileWhoId))
            {
                value.Date = DateTime.Now;
                _profileRepository.AddProfileAction(value);
            }
            result.Profile = _profileRepository.GetProfile(_applicationManager.CurrentUser.ProfileId);
            result.ProfileAction = _profileRepository.GetProfileAction(value.ProfileActionId);
            result.ProfileActionComments = new List<ProfileActionComment>();
            return PartialView("LoadAction", result);
        }

        [HttpPost]
        public void RemoveAction(ProfileAction value)
        {
            var check = _profileRepository.GetProfileAction(value.ProfileActionId);
            if ((check != null) && (_applicationManager.CurrentUser.ProfileId == check.ProfileWhoId) ||
                (_applicationManager.CurrentUser.ProfileId == check.ProfileId))
            {
                _profileRepository.RemoveProfileAction(value.ProfileActionId);
            }
        }

        [HttpPost]
        public ActionResult AddActionLike(ProfileActionLike value)
        {
            var result = new LoadProfileActionHeadResult();
            _profileRepository.AddProfileActionLike((long) value.ProfileActionId,
                (long) _applicationManager.CurrentUser.ProfileId);
            result.Profile = _profileRepository.GetProfile((long) _applicationManager.CurrentUser.ProfileId);
            result.ProfileAction = _profileRepository.GetProfileAction((long) value.ProfileActionId);
            return PartialView("LoadActionHead", result);
        }

        [HttpPost]
        public ActionResult RemoveActionLike(ProfileActionLike value)
        {
            var result = new LoadProfileActionHeadResult();
            _profileRepository.RemoveProfileActionLike((long) value.ProfileActionId,
                (long) _applicationManager.CurrentUser.ProfileId);
            result.Profile = _profileRepository.GetProfile((long) _applicationManager.CurrentUser.ProfileId);
            result.ProfileAction = _profileRepository.GetProfileAction((long) value.ProfileActionId);
            return PartialView("LoadActionHead", result);
        }

        #region Comment

        [HttpPost]
        public ActionResult AddComment(ProfileActionComment value)
        {
            var result = new LoadCommentResult
            {
                Profile = _profileRepository.GetProfile(_applicationManager.CurrentUser.ProfileId),
                Action = _profileRepository.GetProfileAction((long) value.ProfileActionId)
            };

            if (_applicationManager.CurrentUser.ProfileId == value.ProfileId)
            {
                value.Date = DateTime.Now;
                var id = _profileRepository.AddProfileActionComment(value);
                result.ProfileActionComment = _profileRepository.GetProfileActionsComment(id);
            }
            return PartialView("LoadComment", result);
        }

        [HttpPost]
        public void RemoveComment(ProfileActionComment value)
        {
            var check = _profileRepository.GetProfileActionsComment(value.ProfileActionCommentId);
            if (check != null)
            {
                var check2 = _profileRepository.GetProfileAction((long) check.ProfileActionId);
                if ((_applicationManager.CurrentUser.ProfileId == check.ProfileId) ||
                    (_applicationManager.CurrentUser.ProfileId == check2.ProfileId))
                {
                    _profileRepository.RemoveProfileActionComment(value.ProfileActionCommentId);
                }
            }
        }

        [HttpPost]
        public ActionResult AddActionCommentLike(ProfileActionCommentLike value)
        {
            _profileRepository.AddProfileActionCommentLike((long) value.ProfileActionCommentId,
                (long) _applicationManager.CurrentUser.ProfileId);
            var result = new LoadCommentResult
            {
                Profile = _profileRepository.GetProfile(_applicationManager.CurrentUser.ProfileId),
                ProfileActionComment = _profileRepository.GetProfileActionsComment((long) value.ProfileActionCommentId)
            };
            if (result.ProfileActionComment != null)
            {
                result.Action = _profileRepository.GetProfileAction((long) result.ProfileActionComment.ProfileActionId);
            }
            result.Action = new ProfileAction();
            return PartialView("LoadComment", result);
        }

        [HttpPost]
        public ActionResult RemoveActionCommentLike(ProfileActionCommentLike value)
        {
            _profileRepository.RemoveProfileActionCommentLike((long) value.ProfileActionCommentId,
                (long) _applicationManager.CurrentUser.ProfileId);
            var result = new LoadCommentResult
            {
                Profile = _profileRepository.GetProfile(_applicationManager.CurrentUser.ProfileId),
                ProfileActionComment = _profileRepository.GetProfileActionsComment((long) value.ProfileActionCommentId)
            };
            if (result.ProfileActionComment != null)
            {
                result.Action = _profileRepository.GetProfileAction((long) result.ProfileActionComment.ProfileActionId);
            }
            result.Action = new ProfileAction();
            return PartialView("LoadComment", result);
        }

        public ActionResult LoadComments(long? id)
        {
            var result = new LoadCommentsResult
            {
                ProfileActionComments = _profileRepository.GetProfileActionsComments((long) id),
                Profile = _profileRepository.GetProfile(_applicationManager.CurrentUser.ProfileId)
            };
            if (result.ProfileActionComments.Count > 0)
            {
                result.Action =
                    _profileRepository.GetProfileAction(
                        (long) result.ProfileActionComments.FirstOrDefault().ProfileActionId);
            }
            else
            {
                result.Action = new ProfileAction();
            }
            return PartialView(result);
        }

        #endregion
    }
}