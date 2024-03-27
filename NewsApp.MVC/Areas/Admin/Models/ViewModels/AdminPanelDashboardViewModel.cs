﻿using NewsApp.CORE.ViewModels.PostViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;

namespace NewsApp.MVC.Areas.Admin.Models.ViewModels
{
    public class AdminPanelDashboardViewModel
    {
        public int PublicPostCount { get; set; }
        public int PrivatePostCount { get; set; }
        public int TotalPostCount { get; set; }
        public int UnApprovedPostCount { get; set; }
        public int TotalUserCount { get; set; }
        public int UnApprovedUserCount { get; set; }
    }
}
