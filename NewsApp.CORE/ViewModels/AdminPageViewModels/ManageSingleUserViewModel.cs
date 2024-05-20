﻿using NewsApp.CORE.ViewModels.PostViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.AdminPageViewModels
{
    public class ManageSingleUserViewModel
    {
        public AppUserViewModel User { get; set; }
        public List<PostViewModel> Posts { get; set; }

    }
}
