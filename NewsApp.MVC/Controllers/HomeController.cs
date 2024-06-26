using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.ProjectCache;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.RequestModels.UserRequestModels;
using NewsApp.MVC.Extensions;
using NewsApp.MVC.Models;
using NewsApp.SERVICE.Services.Abstract;
using System.Diagnostics;
using System.Security.Claims;

namespace NewsApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly ICategoryService _categoryService;
        private readonly IAppUserService _appUserService;
        public HomeController(
            ILogger<HomeController> logger,
            IPostService postService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            ICategoryService categoryService,
            IAppUserService appUserService
            )
        {
            _logger = logger;
            _postService = postService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _categoryService = categoryService;
            _appUserService = appUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var allNews = await _postService.GettAllPosts();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                return View(allNews.Data);
            }
            allNews.Data = allNews.Data.Where(x => x.IsSubscriberOnly == false).ToList();
            return View(allNews.Data);
        }


        [HttpGet("user/register")]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpGet("news/detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var news = await _postService.GetSinglePostById(id);
            return View(news.Data);
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> SignUp(AppUserRegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!model.PrivacyPolicy)
            {
                ModelState.AddModelError(nameof(model.PrivacyPolicy), "Gizlilik politikas�n� kabul etmelisiniz.");
                return View(model);
            }

            var identityResult = await _userManager
                                        .CreateAsync(
                                                    new AppUser() {
                                                        UserName = model.Username,
                                                        Name=model.Name,
                                                        Surname = model.Surname,
                                                        Email = model.Email,
                                                        BirthDate = model.BirthDate,
                                                        Phone = model.Phone,
                                                        HomeLand = model.HomeLand,
                                                        CreatedDate = DateTime.Now,
                                                    }
                                                    ,model.Password
                                                    );

            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(_ => _.Description).ToList());
            }

            var exchangeExpireClaims = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString());
            var user = await _userManager.FindByNameAsync(model.Username);

            TempData["SuccessMessage"] = "User successfully registered.";
            return RedirectToAction(nameof(HomeController.SignUp));

        }
        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginRequestModel model,string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if(hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email ya da �ifre yanl��.");
                return View();
            }

            if(hasUser.IsDeleted == true)
            {
                ModelState.AddModelError(string.Empty, "dailynews@support.com mail adresi ile ileti�ime ge�iniz.");
                return View();
            }


            if(hasUser.IsSubscriber == false)
            {
                ModelState.AddModelError(string.Empty, "�yelik i�lemi hen�z y�netici taraf�ndan onaylanmad�.");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);
            if(signInResult.Succeeded)
            {

                return Redirect(returnUrl!);
            }

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "Hesab�n�z 3 dakika ask�ya al�nd�." });
                return View();
            }


            ModelState.AddModelErrorList(new List<string>() { $"Email ya da �ifre yanl��. Ba�ar�s�z giri� say�s�: {await _userManager.GetAccessFailedCountAsync(hasUser)}" });
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(AppUserForgetPasswordRequestModel model)
        {
            var hasUser =await _userManager.FindByEmailAsync(model.Email);
            if(hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Bu email adersine sahip bir kullan�c� bulunmamaktad�r.");
                return View();
            }


            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);

            //Email Service Burada Devreye girer.
            await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);
            TempData["success"] = "�ifre s�f�rlama linki e posta adresinize g�nderilmi�tir.";

            return RedirectToAction(nameof(ForgetPassword));
        }

        [HttpGet("posts/{categoryName}")]
        public async Task<IActionResult> CategoryPage(string categoryName)
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            var result = await _postService.GetPostsByCategory(categoryName);

            if (User.Identity.IsAuthenticated)
            {
                return View(result.Data);
            }
            result.Data = result.Data.Where(x => x.IsSubscriberOnly == false).ToList();
            return View(result.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
