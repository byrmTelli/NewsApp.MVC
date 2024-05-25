using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.AdminRequestModels;
using NewsApp.CORE.RequestModels.UserRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IAppUserService
    {
        Task<Response<AppUserViewModel>> AssignCategoryToUser(AssingCategoryOrRoleToUserViewModel request);
        Task<Response<List<AppUserViewModel>>> GetAllUsers();
        Task<Response<NoDataViewModel>> ApproveUsersAccount(ApproveUserRequestModel request);
        Task<Response<NoDataViewModel>> UpdateUser(AppUserUpdateRequestModel request);
        Task<Response<AppUserViewModel>> GetSingleUserById(string userId);
        Task<Response<List<AppUserViewModel>>> GetDirectorsOfCategory(string categoryId);
        Task<Response<List<AppUserViewModel>>> GetAuthorsOfCategory(string categoryId);
        Task<Response<List<AppUserViewModel>>> GetAllUsersWithLinq();
    }
}
