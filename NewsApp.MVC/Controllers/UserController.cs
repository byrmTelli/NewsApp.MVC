using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.UserRequestModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using NewsApp.MVC.Extensions;
using NewsApp.SERVICE.Services.Abstract;
using System.Security.Claims;

namespace NewsApp.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;  
        private readonly IFileProvider _fileProvider;
        private readonly ICategoryService _categoryService;


        public UserController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService,
            IFileProvider fileProvider,
            ICategoryService categoryService
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
            _fileProvider = fileProvider;
            _categoryService = categoryService;
        }
        [HttpGet("/linq")]
        public async Task<IActionResult> GetUsersWithLinq()
        {
            var allUsers = await _appUserService.GetAllUsersWithLinq();
            return View(allUsers.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userClaims = User.Claims.ToList();

            var mail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            var currentUser =await _userManager.FindByNameAsync(User.Identity!.Name!);
            var currentUserRoles =await _userManager.GetRolesAsync(currentUser!);
            var userCategory = await _categoryService.GetUsersCategory(currentUser.Id);

            var userViewModel = new AppUserViewModel()
            {
                Email = currentUser.Email,
                UserName = currentUser.UserName,
                Phone = currentUser.PhoneNumber,
                Name = currentUser.Name,
                Surname = currentUser.Surname,
                HomeLand =currentUser.HomeLand,
                BirthDate = currentUser.BirthDate,
                Roles = currentUserRoles?.ToList(),
                UserCategory = userCategory

            };
            //Image converting
            if (currentUser.Image != null)
            {
                var image = currentUser.Image;
                var imageStream = new MemoryStream(image);
                var Image = Convert.ToBase64String(image);
                var convertedImage = "data:image/jpg;base64," + Image;
                userViewModel.Image = convertedImage;
                return View(userViewModel);
            }

            return View(userViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> SignOut(string returnurl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(AppUserPasswordChangeRequestModel request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski şifreniz yanlış.");
                return View();
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.Password);
            if (!resultChangePassword.Succeeded)
            {
                ModelState.AddModelErrorList(resultChangePassword.Errors);
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.Password, true, false);
            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            var currentUser =await _userManager.FindByNameAsync(User.Identity.Name);
            var currentUserViewModel =await _appUserService.GetSingleUserById(currentUser!.Id);
            return View(currentUserViewModel.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUserUpdateRequestModel request)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
                request.Id = currentUser.Id;
                await _appUserService.UpdateUser(request);

                var updatedUserViewModel = await _appUserService.GetSingleUserById(currentUser.Id);

                return RedirectToAction(nameof(UserController.UpdateUser));
            }

            return RedirectToAction(nameof(UserController.UpdateUser));

        }
        public IActionResult AccessDenied(string returnUrl)
        {
            string message = string.Empty;


            message = "Bu sayfayı görmeye yetkiniz yoktur. Yetki almak için yöneticiniz ile görüşebilirsiniz.";
            ViewBag.message = message;

            return View();
        }
    }
}
