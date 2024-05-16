using NewsApp.CORE.ViewModels.PostViewModels;

namespace NewsApp.MVC.Areas.Admin.Models.ManagePostsViewModels
{
    public class ManagePostsViewModel
    {
        public List<PostViewModel> AllPosts { get; set; } = new List<PostViewModel>();


    }
}
