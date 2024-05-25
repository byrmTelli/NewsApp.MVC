﻿using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface IAppUserDal:IEntityRepository<AppUser>
    {
        Task<List<AppUserViewModel>> GetAllUsersWithCategoryAndRole();
        Task<NoDataViewModel> DeleteUser(string id);
        Task<NoDataViewModel> ReActiveUser(string id);
        Task<Response<AppUserViewModel>> AssignCategoryToUserAsync(AssingCategoryOrRoleToUserViewModel request);
        Task<int> NewUserCountMontly();
    }
}
