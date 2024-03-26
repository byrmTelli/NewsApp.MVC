using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.CORE.DBModels;
using NewsApp.SERVICE.Services.Abstract;

namespace NewsApp.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService _categoryService;
        public AdminController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IAppUserService userService,
            ICategoryService categorySerivce
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserService = userService;
            _categoryService = categorySerivce;
        }
        public async Task<IActionResult> Index()
        {
            var allUsers = await _appUserService.GetAllUsers();
            return View(allUsers.Data);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AssignCategoryToUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            ViewBag.Categories = await _categoryService.GetUsersCategory(user.Id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategoryToUser(string userId, string categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _appUserService.AssignCategoryToUser(userId, categoryId);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApproveUsersAccount(string userId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _appUserService.ApproveUsersAccount(userId);
            return RedirectToAction("Index", "Admin");
        }
    }
}
