using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.RoleRequestModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
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

        public async Task<IActionResult> AssignRoleToUser()
        {
           var allRoles =await _appRoleService.GetAllRoles();
            ViewBag.AllRoles = allRoles.Data;
            var allUsers = await _appUserService.GetAllUsers();
            return View(allUsers.Data);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);


            var roles = await _roleManager.Roles.ToListAsync();

            var userRoles = await _userManager.GetRolesAsync(currentUser!);


            var roleViewModelList = new List<AssignRoleToUserViewModel>();

            foreach (var role in roles)
            {
                var assignRoleToUserViewmodel = new AssignRoleToUserViewModel() { Id = role.Id, Name = role.Name };

                if (userRoles.Contains(role.Name!))
                {
                    assignRoleToUserViewmodel.isRoleExist = true;
                }
                roleViewModelList.Add(assignRoleToUserViewmodel);
            }
            return View(roleViewModelList);
        }
    }
}
