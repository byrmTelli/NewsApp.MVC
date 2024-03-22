using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.UserRequestModels;
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
        Task<Response<AppUserViewModel>> AssignCategoryToUser(string userId, string categoryId);
        Task<Response<List<AppUserViewModel>>> GetAllUsers();
        Task<Response<NoDataViewModel>> ApproveUsersAccount(string useId);
        Task<Response<NoDataViewModel>> UpdateUser(AppUserUpdateRequestModel request);
        Task<Response<AppUserViewModel>> GetSingleUserById(string userId);
    }
}
