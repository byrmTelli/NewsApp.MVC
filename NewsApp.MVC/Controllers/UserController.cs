using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.UserRequestModels;
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


        public UserController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService,
            IFileProvider fileProvider
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appUserService = appUserService;
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            var userClaims = User.Claims.ToList();

            var mail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            var currentUser = _userManager.FindByNameAsync(User.Identity!.Name!);

            var userViewModel = new AppUserViewModel()
            {
                Email = currentUser.Result.Email,
                UserName = currentUser.Result.UserName,
                Phone = currentUser.Result.PhoneNumber,
                Name = currentUser.Result.Name,
                Surname = currentUser.Result.Surname,
                HomeLand =currentUser.Result.HomeLand,
                BirthDate = currentUser.Result.BirthDate,

            };

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
            var currentUserViewModel =await _appUserService.GetSingleUserById(currentUser.Id);
            return View(currentUserViewModel.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AppUserUpdateRequestModel request)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            request.Id = currentUser.Id;
            await _appUserService.UpdateUser(request);

            var updatedUserViewModel = await _appUserService.GetSingleUserById(currentUser.Id);

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
