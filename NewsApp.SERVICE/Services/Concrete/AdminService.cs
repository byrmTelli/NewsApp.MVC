using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using NewsApp.DAL.Context;
using NewsApp.SERVICE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public AdminService(AppDbContext dbContext,UserManager<AppUser> userManager)
        {
            _dbContext = dbContext; 
            _userManager = userManager;
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
                    Roles = data.Roles.ToList()
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
    }
}
