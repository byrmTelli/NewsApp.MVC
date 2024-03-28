using NewsApp.CORE.ViewModels.UserViewModels;

namespace NewsApp.CORE.ViewModels.AdminPageViewModels
{
    public class ManageUserViewModel
    {
        public int WriterCount { get; set; }
        public int ManagerCount { get; set; }
        public int NonApprovedUserCount { get; set; }
        public int TotalUser { get; set; }
        public List<AppUserViewModel> UserList { get; set; }
    }
}
