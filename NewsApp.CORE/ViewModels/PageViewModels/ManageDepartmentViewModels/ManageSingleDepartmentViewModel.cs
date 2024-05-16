using NewsApp.CORE.DBModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;

namespace NewsApp.MVC.Areas.Admin.Models.ManageDepartmentViewModels
{
    public class ManageSingleDepartmentViewModel
    {
        public List<AppUserViewModel>? Director { get; set; }
        public List<AppUserViewModel> Authors { get; set; } = new List<AppUserViewModel>();
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
        public CategoryViewModel Department { get; set; }
    }
}
