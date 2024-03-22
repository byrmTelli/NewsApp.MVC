using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(
            ILogger<HomeController> logger,
            IPostService postService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService
            )
        {
            _logger = logger;
            _postService = postService;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            var allNews = await _postService.GettAllNews();
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                return View(allNews.Data);
            }
            allNews.Data = allNews.Data.Where(x => x.IsSubscriberOnly == false).ToList();
            return View(allNews.Data);
        }


        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpGet("news/detail/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var news = await _postService.GetSingleNewsById(id);
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager
                                        .CreateAsync(
                                                    new AppUser() {
                                                        UserName = model.Username,
                                                        Name=model.Name,
                                                        Surname = model.Surname,
                                                        Email = model.Email,
                                                        BirthDate = model.BirthDate
                                                    }
                                                    ,model.Password
                                                    );

            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(_ => _.Description).ToList());
            }

            var exchangeExpireClaims = new Claim("ExchangeExpireDate", DateTime.Now.AddDays(10).ToString());
            var user = await _userManager.FindByNameAsync(model.Username);

            //var claimResult = await _userManager.AddClaimAsync(user!, exchangeExpireClaims);

            //if (!claimResult.Succeeded)
            //{
            //    ModelState.AddModelErrorList(claimResult.Errors.Select(_ => _.Description).ToList());
            //}

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
                ModelState.AddModelError(string.Empty, "Email ya da þifre yanlýþ.");
                return View();
            }


            if(hasUser.IsSubscriber == false)
            {
                ModelState.AddModelError(string.Empty, "Üyelik iþlemi henüz yönetici tarafýndan onaylanmadý.");
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "Hesabýnýz 3 dakika askýya alýndý." });
                return View();
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Email ya da þifre yanlýþ. Baþarýsýz giriþ sayýsý{await _userManager.GetAccessFailedCountAsync(hasUser)}" });
            }

            return Redirect(returnUrl!);

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
                ModelState.AddModelError(String.Empty, "Bu email adersine sahip bir kullanýcý bulunmamaktadýr.");
                return View();
            }


            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);

            //Email Service Burada Devreye girer.
            await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);
            TempData["success"] = "Þifre sýfýrlama linki e posta adresinize gönderilmiþtir.";

            return RedirectToAction(nameof(ForgetPassword));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
