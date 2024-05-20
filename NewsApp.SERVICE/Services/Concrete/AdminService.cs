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
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserDal _appUserDal;
        private readonly IPostDal _postDal;
        private readonly ICategoryDal _categoryDal;

        public AdminService(
            AppDbContext dbContext,
            UserManager<AppUser> userManager,
            IAppUserDal appUserDal,
            IPostDal postDal,
            ICategoryDal categoryDal
            )
        {
            _dbContext = dbContext; 
            _userManager = userManager;
            _appUserDal = appUserDal;
            _postDal = postDal;
            _categoryDal = categoryDal;
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
        public async Task<Response<DashboardViewModel>> GetDashboardData()
        {
            var writers = await _userManager.GetUsersInRoleAsync("writer");
            var writerCount = writers.Count();
            var managers = await _userManager.GetUsersInRoleAsync("director");
            var managerCount = managers.Count();
            var users = await _dbContext.Users.ToListAsync();
            var userCount = users.Count();
            var posts = await _dbContext.Posts.Include(x => x.Category).ToListAsync();
            var postsCount = posts.Count();

            // Kategorilere göre post sayılarını hesaplama
            var postCountsByCategory = posts.GroupBy(x => x.Category.Name)
                                             .Select(g => new { Category = g.Key, Count = g.Count() })
                                             .ToList();

            var barChartTitles = postCountsByCategory.Select(x => x.Category).ToList();
            var barChartData = postCountsByCategory.Select(x => x.Count).ToList();

            var result = new DashboardViewModel()
            {
                UserCount = userCount,
                PostCount = postsCount,
                ManagerCount = managerCount,
                WriterCount = writerCount,
                BarChartTitles = barChartTitles,
                BarChartData = barChartData,
                PieChartTitles = barChartTitles,
                PieChartData = barChartData
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
            var writers = await _userManager.GetUsersInRoleAsync("writer");
            var managers = await _userManager.GetUsersInRoleAsync("manager");
            var usersWithRoles = await _dbContext.Users
                .Select(user => new
                {
                    User = user,
                    Roles = _userManager.GetRolesAsync(user).Result
                })
                .Select(data => new AppUserViewModel
                {
                    Id = data.User.Id,
                    Name = data.User.Name,
                    Surname = data.User.Surname,
                    UserName = data.User.UserName,
                    Phone = data.User.Phone,
                    Email = data.User.Email,
                    IsSubcriber = data.User.IsSubscriber,
                    Roles = data.Roles.ToList(),
                    IsDeleted = data.User.IsDeleted
                })
                .ToListAsync();


            var manageUserViewModel = new ManageUserViewModel()
            {
                WriterCount = writers.Count,
                ManagerCount = managers.Count,
                TotalUser = usersWithRoles.Count,
                UserList = usersWithRoles
            };
            return Response<ManageUserViewModel>.Success(manageUserViewModel, 200);
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
