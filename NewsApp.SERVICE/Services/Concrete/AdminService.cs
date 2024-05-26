using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using NewsApp.CORE.ViewModels.PageViewModels;
using NewsApp.DAL.Context;
using NewsApp.SERVICE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.MVC.Areas.Admin.Models.ManageDepartmentViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.DAL.Concrete;
using NewsApp.DAL.Abstract;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserDal _appUserDal;
        private readonly IPostDal _postDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IApproveUserDal _approveUserDal;
        private readonly IApprovePostDal _approvePostDal;

        public AdminService(
            UserManager<AppUser> userManager,
            IAppUserDal appUserDal,
            IPostDal postDal,
            ICategoryDal categoryDal,
            IApproveUserDal approveUserDal,
            IApprovePostDal approvePostDal
            )
        {
            _userManager = userManager;
            _appUserDal = appUserDal;
            _postDal = postDal;
            _categoryDal = categoryDal;
            _approveUserDal = approveUserDal;
            _approvePostDal = approvePostDal;
        }

        public async Task<Response<NoDataViewModel>> ActivateCateory(string categoryId)
        {
            return await _categoryDal.ActiveCategory(categoryId);
        }

        public async Task<Response<NoDataViewModel>> DeleteUser(string id)
        {
            await _appUserDal.DeleteUser(id);
            return Response<NoDataViewModel>.Success(204);
        }

        public async Task<Response<List<AppUserViewModel>>> GetAllUsers()
        {
            var result =await _appUserDal.GetAllUsersWithCategoryAndRole();
            return Response<List<AppUserViewModel>>.Success(result,200);
        }

        public async Task<Response<DashboardViewModel>> GetDashboardData()
        {
            var writers = await _userManager.GetUsersInRoleAsync("writer");
            var writerCount = writers.Count();
            var managers = await _userManager.GetUsersInRoleAsync("director");
            var managerCount = managers.Count();
            var users = await _appUserDal.GetAllUsersWithCategoryAndRole();
            var userCount = users.Count();
            var posts = await _postDal.GetAllPosts();
            var postsCount = posts.Count();

            var postCountsByCategory = posts.GroupBy(x => x.Category.Name)
                                             .Select(g => new { Category = g.Key, Count = g.Count() })
                                             .ToList();

            var barChartTitles = postCountsByCategory.Select(x => x.Category).ToList();
            var barChartData = postCountsByCategory.Select(x => x.Count).ToList();

            var MonthlyUserCount = await _appUserDal.NewUserCountMontly();
            var MonthlyPostApprovalCount = await _postDal.GetMontlyApprovedPostCount();
            var approveUserRecords = await _approveUserDal.GetListOfApproveRecords();
            var approvePostRecords = await _approvePostDal.GetListOfApprovePosts();
            var result = new DashboardViewModel()
            {
                UserCount = userCount,
                PostCount = postsCount,
                ManagerCount = managerCount,
                WriterCount = writerCount,
                BarChartTitles = barChartTitles,
                BarChartData = barChartData,
                PieChartTitles = barChartTitles,
                PieChartData = barChartData,
                ApproveUserRecords = approveUserRecords.Data,
                ApprovePostRecords = approvePostRecords.Data,
                MontlyUserCount = MonthlyUserCount,
                MontlyPostApprovalCount = MonthlyPostApprovalCount
            };

            return Response<DashboardViewModel>.Success(result, 200);
        }
        public async Task<Response<ManageSingleUserViewModel>> GetSingleUser(string id)
        {
            var result = await _postDal.GetAllPostsOfUser(id);
            return result;
        }
        public async Task<Response<ManageUserViewModel>> ManageUsers()
        {
            var result = await _appUserDal.ManagUsers();
            return result;
        }
        public async Task<Response<NoDataViewModel>> ReActiveUser(string id)
        {
            await _appUserDal.ReActiveUser(id);
            return Response<NoDataViewModel>.Success(200);

        }
        public async Task<Response<NoDataViewModel>> RemoveCategory(string categoryId)
        {
            return await _categoryDal.RemoveCategory(categoryId);
        }
    }
}
