using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewsApp.CORE.DBModels;


namespace NewsApp.MVC.Extensions
{
    public class LoginUserExtensionFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LoginUserExtensionFilter(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (userName != null)
            {
                var currentUser = _userManager.FindByNameAsync(userName).Result;
                if (currentUser != null)
                {
                    // Controller tipinde bir nesne alalım
                    var controller = context.Controller as Controller;
                    // ViewData veya ViewBag'e kullanıcı bilgilerini ve profil fotoğrafını ekleyin
                    controller.ViewData["CurrentUser"] = currentUser.Name+" "+currentUser.Surname;
                    controller.ViewData["ProfileImage"] = currentUser.Image == null ? null : "data:image/jpg;base64," + Convert.ToBase64String(currentUser.Image); // Profil fotoğrafı örneği
                }
            }
        }

    }
}
