using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.RoleRequestModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using NewsApp.MVC.Areas.Admin.Controllers;
using NewsApp.MVC.Extensions;
using NewsApp.SERVICE.Services.Abstract;

namespace NewsApp.MVC.Controllers
{
    [Authorize(Roles ="admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly IAppRoleService _appRoleService;

        public RoleController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IAppUserService appUserService,
            IAppRoleService appRoleService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserService = appUserService;
            _appRoleService = appRoleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RoleCreate(AppRoleCreateRequestModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }

            return RedirectToAction(nameof(RoleController.Index));
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, string roleId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);

            if (currentUser == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            if (userRoles.Contains(role.Name))
            {
                ModelState.AddModelError(string.Empty, "User already has this role.");
                return View();
            }
            await _userManager.AddToRoleAsync(currentUser, role.Name);

            return RedirectToAction("AssignCategoryToUser", "Admin", new { area = "Admin" });

        }

    }
}
