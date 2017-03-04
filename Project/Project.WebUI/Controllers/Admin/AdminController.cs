using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.WebUI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Business.DataAccess.Public.Entities.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Project.WebUI.Controllers.Admin
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private ApplicationManager _userManager;
        public ApplicationManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationManager>();
            }
            set
            {
                _userManager = value;
            }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            set
            {
                _roleManager = value;
            }
        }
        
        public int PageSize = 10;
        
        //public AdminController()            
        //{
        //    RoleManager = HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationManager>();
        //}
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Users(string sort, string userNameFilter, string roleFilter, int page = 1)
        {
            AdminUsersViewResult result = new AdminUsersViewResult();
            result.Users = new List<ApplicationUserEntity>();
            var users = UserManager.Users.AsQueryable();
            users = users.Where(m => m.UserName.Contains(userNameFilter) || userNameFilter == null);
            users = users.Where(m => m.Roles.Select(k => k.RoleId).Contains(roleFilter) || roleFilter == "0" || roleFilter == null);
            switch (sort)
            {
                case "UserNameDesc": users = users.OrderByDescending(m => m.UserName); break;
                case "UserNameAsc": users = users.OrderBy(m => m.UserName); break;
                case "LastLoginTimeDesc": users = users.OrderByDescending(m => m.LastLoginTime); break;
                case "LastLoginTimeAsc": users = users.OrderBy(m => m.LastLoginTime); break;
                case "RegistrationDateDesc": users = users.OrderByDescending(m => m.RegistrationDate); break;
                case "RegistrationDateAsc": users = users.OrderBy(m => m.RegistrationDate); break;
                case "LoginProviderDesc": users = users.OrderByDescending(m => m.Logins.FirstOrDefault().LoginProvider); break;
                case "LoginProviderAsc": users = users.OrderBy(m => m.Logins.FirstOrDefault().LoginProvider); break;
                default: users = users.OrderBy(m => m.UserName); break;
            }
            result.Users = users
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            result.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = users.Count()
            };
            result.Sorted = sort;
            result.Roles = RoleManager.Roles.ToList();
            result.Roles.Add(new IdentityRole { Id = "0", Name = "Фильтр ролей" });
            result.Roles = result.Roles.OrderBy(m => Convert.ToInt32(m.Id)).ToList();
            result.UserNameFilter = userNameFilter;
            result.RoleFilter = roleFilter;
            return View(result);
        }
        public ActionResult UserEdit(string id,string returnUrl)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            AdminUserEditViewResult result = new AdminUserEditViewResult();
            var claims = UserManager.GetClaims(id);
            var banned=claims.FirstOrDefault(m=>m.Type == "Banned");
            result.Roles = RoleManager.Roles.ToList();
            result.User = UserManager.FindById(id);
            result.returnUrl = returnUrl;
            result.Banned = Convert.ToBoolean(result.User.Banned);
            if (result.User != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }

        }
        [HttpPost]
        public async Task<ActionResult> UserEdit(AdminUserEditViewResult model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserEntity user = UserManager.FindById(model.User.Id);
                if (user != null)
                {
                    //Поиск claim Banned
                                    
                    //Добавляем, если необходимо
                    if ((model.Banned == true) && (user.Banned!=true))
                    {
                        user.Banned = true;
                    }
                    //Удаляем, если необходимо
                    else if (user.Banned == true) 
                    {
                       user.Banned = false;
                    }
                    //Очищаем все роли пользователя
                    foreach (var role in UserManager.GetRoles(user.Id).ToList())
                    {
                        await UserManager.RemoveFromRoleAsync(user.Id, role);
                    }
                    //Добавляем необходимы роли пользователя
                    foreach (var role in model.SelectedRoles.Where(m => m.Checked).ToList())
                    {
                        await UserManager.AddToRoleAsync(user.Id, role.Name);
                    }
                    model.Roles = RoleManager.Roles.ToList();
                    TempData["toastrMessage"] = String.Format("Пользователь {0} изменен", user.UserName);
                    TempData["toastrType"] = "success";
                    UserManager.Update(user);
                    return View("UserEdit", model);
                }
                else
                {
                    return View("UserEdit", model);
                }
            }
            else
            {
                return View("UserEdit", model);
            }
        }
    }
}