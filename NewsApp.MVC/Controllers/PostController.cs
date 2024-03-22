using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.NewsRequestModels;
using NewsApp.SERVICE.Services.Abstract;

namespace NewsApp.MVC.Controllers
{
    [Authorize(Roles = "admin,director,sports,politic,culture,technology,magazine")]
    public class PostController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IPostService _postService;
        public PostController(
            ICategoryService categoryService,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IPostService postService
            )
        {
            _categoryService = categoryService;
            _userManager = userManager;
            _roleManager = roleManager;
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {

            var currentUser =await _userManager.FindByNameAsync(User.Identity.Name);
            var userPosts =await _postService.GetAllPostsOfUser(currentUser.Id.ToString());
            return View(userPosts.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            ViewBag.Categories = await _categoryService.GetUsersCategory(currentUser.Id); // Kategorileri view'e aktar

            ViewBag.CurrentUserId = currentUser?.Id; // Oturumu açmış kullanıcının ID'sini view'e aktar

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var result = await _postService.Create(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
