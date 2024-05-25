using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IAdminService
    {
        Task<Response<ManageUserViewModel>> ManageUsers();
        Task<Response<DashboardViewModel>> GetDashboardData();
        Task<Response<NoDataViewModel>> DeleteUser(string id);
        Task<Response<NoDataViewModel>> ReActiveUser(string id);
        Task<Response<ManageSingleUserViewModel>> GetSingleUser(string id);
        Task<Response<NoDataViewModel>> RemoveCategory(string categoryId);
        Task<Response<NoDataViewModel>> ActivateCateory(string categoryId);
        Task<Response<List<AppUserViewModel>>> GetAllUsers();
    }
}
