using System;
using System.Collections.Generic;
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
            value.Date = DateTime.Now;
            _profileService.AddProfileAction(value, (long) _applicationManager.CurrentUser.ProfileId);
            var result = new AddActionResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileAction = _profileService.GetProfileAction(value.ProfileActionId),
                ProfileActionComments = new List<ProfileActionComment>()
            };
            return PartialView("LoadAction", result);
        }

        [HttpPost]
        public void RemoveAction(ProfileAction value)
        {
            _profileService.RemoveProfileAction(value.ProfileActionId, (long) _applicationManager.CurrentUser.ProfileId);
        }

        [HttpPost]
        public ActionResult AddActionLike(ProfileActionLike value)
        {
            _profileService.AddActionLike((long) value.ProfileActionId,
                (long) _applicationManager.CurrentUser.ProfileId);

            var result = new LoadProfileActionHeadResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileAction = _profileService.GetProfileAction((long) value.ProfileActionId)
            };
            return PartialView("LoadActionHead", result);
        }

        [HttpPost]
        public ActionResult RemoveActionLike(ProfileActionLike value)
        {
            _profileService.RemoveActionLike((long) value.ProfileActionId,
                (long) _applicationManager.CurrentUser.ProfileId);

            var result = new LoadProfileActionHeadResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileAction = _profileService.GetProfileAction((long) value.ProfileActionId)
            };
            return PartialView("LoadActionHead", result);
        }

        #region Comment

        [HttpPost]
        public ActionResult AddComment(ProfileActionComment value)
        {
            if (_applicationManager.CurrentUser.ProfileId != value.ProfileId)
            {
                throw new AccessViolationException(nameof(value.ProfileId));
            }
            value.Date = DateTime.Now;
            var actionCommentId = _profileService.AddProfileActionComment(value);

            var result = new LoadCommentResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                Action = _profileService.GetProfileAction((long) value.ProfileActionId),
                ProfileActionComment = _profileService.GetProfileActionsComment(actionCommentId)
            };


            return PartialView("LoadComment", result);
        }

        [HttpPost]
        public void RemoveComment(ProfileActionComment value)
        {
            _profileService.RemoveProfileActionsComment(value.ProfileActionCommentId,
                (long) _applicationManager.CurrentUser.ProfileId);
        }

        [HttpPost]
        public ActionResult AddActionCommentLike(ProfileActionCommentLike value)
        {
            value.ProfileId = _applicationManager.CurrentUser.ProfileId;
            value.Date = DateTime.Now;
            _profileService.AddProfileActionCommentLike(value);

            var result = new LoadCommentResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileActionComment = _profileService.GetProfileActionsComment((long) value.ProfileActionCommentId)
            };
            if (result.ProfileActionComment != null)
            {
                result.Action = _profileService.GetProfileAction((long) result.ProfileActionComment.ProfileActionId);
            }
            result.Action = new ProfileAction();
            return PartialView("LoadComment", result);
        }

        [HttpPost]
        public ActionResult RemoveActionCommentLike(ProfileActionCommentLike value)
        {
            _profileService.RemoveProfileActionCommentLike((long) value.ProfileActionCommentId,
                (long) _applicationManager.CurrentUser.ProfileId);
            var result = new LoadCommentResult
            {
                Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId),
                ProfileActionComment = _profileService.GetProfileActionsComment((long) value.ProfileActionCommentId)
            };
            if (result.ProfileActionComment != null)
            {
                result.Action = _profileService.GetProfileAction((long) result.ProfileActionComment.ProfileActionId);
            }
            result.Action = new ProfileAction();
            return PartialView("LoadComment", result);
        }

        public ActionResult LoadComments(long? id)
        {
            //var result = new LoadCommentsResult
            //{
            //    ProfileActionComments = _profileService.GetProfileActionsComments((long) id),
            //    Profile = _profileService.GetShortProfile((long) _applicationManager.CurrentUser.ProfileId)
            //};
            //if (result.ProfileActionComments.Count > 0)
            //{
            //    result.Action = _profileService.GetProfileAction(
            //            (long) result.ProfileActionComments.FirstOrDefault().ProfileActionId);
            //}
            //else
            //{
            //    result.Action = new ProfileAction();
            //}
            //return PartialView(result);

            throw new NotImplementedException();
        }

        #endregion
    }
}