using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.CategoyRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels;
using NewsApp.CORE.ViewModels.PageViewModels.ManageDepartmentViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using NewsApp.MVC.Areas.Admin.Models.ManageDepartmentViewModels;
using NewsApp.MVC.Extensions;
using NewsApp.SERVICE.Services.Abstract;
using NewsApp.SERVICE.Services.Concrete;

namespace NewsApp.MVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,director")]
    [Area("Admin")]
    [TypeFilter(typeof(LoginUserExtensionFilter))]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAppUserService _appUserService;
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;
        private readonly IAdminService _adminService;
        private readonly IAppRoleService _appRoleService;
        public AdminController(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IAppUserService userService,
            ICategoryService categorySerivce,
            IPostService postService,
            IAdminService adminService,
            IAppRoleService appRoleService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appUserService = userService;
            _categoryService = categorySerivce;
            _postService = postService;
            _adminService = adminService;
            _appRoleService = appRoleService;
        }
        public async Task<IActionResult> Index()
        {
            var managEUsers = await _adminService.ManageUsers();
            return View(managEUsers.Data);
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
            var allUsers = await _appUserService.GetAllUsers();
            var allRoles = await _appRoleService.GetAllRoles();
            var userRoles = await _userManager.GetRolesAsync(user);
            ViewBag.AllRoles = allRoles.Data;
            var allCategories = await _categoryService.GetAllCategories();
            ViewBag.AllCategories = allCategories;
            return View(allUsers.Data);
        }
        [HttpPost]
        public async Task<IActionResult> AssignCategoryToUser(AssingCategoryOrRoleToUserViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AssignCategoryToUser", "Admin");
            }
            await _appUserService.AssignCategoryToUser(request);
            return RedirectToAction("AssignCategoryToUser","Admin");
        }


        [Authorize(Roles = "admin")]
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
        [HttpGet]
        public async Task<IActionResult> ManagePosts()
        {
            var allPosts =await _postService.GettAllPosts();
            return View(allPosts.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }
        [HttpGet("admin/managedepartments")]
        public async Task<IActionResult> ManageDepartments()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var usersRole = await _userManager.GetRolesAsync(currentUser);
            var usersDepartments = await _categoryService.GetUsersCategory(currentUser.Id);
            var categories = await _categoryService.GetAllCategories();
            if (usersRole.Contains("director") && !usersRole.Contains("admin"))
            {
                categories = categories.Where(x => x.Id == usersDepartments.Id).ToList();
            }

            var pageViewModel = new ManageDepartmentsPageViewModel()
            {
                CategoryViewModel = categories
            };
            return View(pageViewModel);
        }
        [HttpGet("admin/department/{departmentId}")]
        public async Task<IActionResult> ManageDepartment(string departmentId)
        {
            //departman
            var department = await _categoryService.GetCategoryById(departmentId);
            //postları al
            var posts = await _postService.GetUnPublishedPostsByCategory(department.Name);
            //director bul
            var directors = await _appUserService.GetDirectorsOfCategory(departmentId);
            // yazarları bul
            var authors = await _appUserService.GetAuthorsOfCategory(departmentId);
            
            var manageDepartmentsViewModel = new ManageSingleDepartmentViewModel()
            {
                Director = directors.Data,
                Authors = authors.Data,
                Posts = posts.Data,
                Department = department
            };
            return View(manageDepartmentsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ApprovePost(string postId,string departmentId)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var usersRole = await _userManager.GetRolesAsync(currentUser);
            var department = await _categoryService.GetCategoryById(departmentId);
            var usersDepartments = await _categoryService.GetUsersCategory(currentUser.Id);

            if (usersRole.Contains("admin"))
            {
                await _postService.ApprovePost(postId);
                return RedirectToAction("ManageDepartment", "Admin", new { departmentId = departmentId.ToUpper() });
            }

            if(usersDepartments.Id == department.Id.ToUpper() && usersRole.Contains("director"))
            {
                await _postService.ApprovePost(postId);
                return RedirectToAction("ManageDepartment", "Admin", new { departmentId = departmentId.ToUpper() });
            }
            return RedirectToAction("ManageDepartment", "Admin", new { departmentId = departmentId.ToUpper() });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(ManageDepartmentsPageViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ManageDepartments", "Admin");
            }

            var response = await _categoryService.CreateCategory(request.CategoryRequestModel);
            return RedirectToAction("ManageDepartments","Admin");
        }
    }
}
