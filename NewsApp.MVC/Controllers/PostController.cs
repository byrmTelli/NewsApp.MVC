using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.NewsRequestModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
using NewsApp.SERVICE.Services.Abstract;

namespace NewsApp.MVC.Controllers
{
    [Authorize(Roles = "admin,writer,director")]
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
            var allCategories = await _categoryService.GetAllCategories();

            var usersCategory = await _categoryService.GetUsersCategory(currentUser!.Id);

            var usersRoles = await _userManager.GetRolesAsync(currentUser);

            if (!usersRoles.Contains("admin"))
            {
                ViewBag.Categories = new List<CategoryViewModel> { usersCategory };
                ViewBag.CurrentUserId = currentUser?.Id;
                return View();
            }
            ViewBag.Categories = allCategories.Data;
            ViewBag.CurrentUserId = currentUser?.Id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostRequestModel model)
        {
            var categories = await _categoryService.GetAllCategories();
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categories.Data;
                return View();
            }

            var user = await _userManager.FindByNameAsync(User.Identity!.Name);
            var usersCategories = await _categoryService.GetUsersCategory(user!.Id);
            var userRoles = await _userManager.GetRolesAsync(user);
            ViewBag.Categories = categories.Data;

            if (userRoles.Contains("admin"))
            {
                await _postService.Create(model);
                return RedirectToAction("Index", "Home");
            }

            if (usersCategories.Id == model.CategoryId)
            {
                await _postService.Create(model);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ModelState.AddModelError("CategoryId", "You are not authorized to create posts in this category.");

                return View();
            }

        }
        [HttpGet("post/update/{postId}")]
        public async Task<IActionResult> Update(string postId)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var usersCategory = await _categoryService.GetUsersCategory(currentUser!.Id);
            var usersRoles = await _userManager.GetRolesAsync(currentUser);
            var allCategories = await _categoryService.GetAllCategories();

            var post = await _postService.GetSinglePostById(postId);

            if (!usersRoles.Contains("admin"))
            {
                ViewBag.Categories = new List<CategoryViewModel> { usersCategory };
                ViewBag.CurrentUserId = currentUser?.Id;
                return View(post.Data);
            }

            ViewBag.Categories = allCategories.Data;
            ViewBag.CurrentUserId = currentUser?.Id;

            return View(post.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PostRequestModel model)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var usersRoles = await _userManager.GetRolesAsync(currentUser);

            if (ModelState.IsValid && (currentUser!.Id == model.CreatorId | usersRoles.Contains("admin")))
            {
                await _postService.UpdatePost(model);
                return RedirectToAction("Index","Home");
            }
            var usersCategory = await _categoryService.GetUsersCategory(currentUser!.Id);
            var allCategories = await _categoryService.GetAllCategories();


            if (!usersRoles.Contains("admin"))
            {
                ViewBag.Categories = new List<CategoryViewModel> { usersCategory };
                ViewBag.CurrentUserId = currentUser?.Id;
                return View();
            }

            ViewBag.Categories = allCategories.Data;
            ViewBag.CurrentUserId = currentUser?.Id;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string postId)
        {
            if(postId !=null) {
            await _postService.Delete(postId);
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [Authorize("admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePermanently(string postId)
        {
            if (postId != null)
            {
                await _postService.DeletePermanently(postId);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet("post/view/{postId}")]
        public async Task<IActionResult> Inspect(string postId)
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var post =await _postService.GetSinglePostById(postId);
            return View(post.Data);
        }



    }
}
