using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.UserRequestModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
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
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;
        public AppUserService(UserManager<AppUser> userManager, AppDbContext context,RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<Response<AppUserViewModel>> AssignCategoryToUser(string userId, string categoryId)
        {
            var isUserExist = await _userManager.FindByIdAsync(userId);
            if (isUserExist == null)
            {
                throw new ArgumentNullException(nameof(isUserExist));
            }

            var isCategoryExist = await _context.Categories.Where(_ => _.Id.ToString() == categoryId).FirstOrDefaultAsync();

            if (isCategoryExist == null)
            {
                throw new ArgumentNullException(nameof(isCategoryExist));
            }


            isUserExist.UserCategories.Add(new AppUserCategory() { CategoryId = Guid.Parse(categoryId) });
            await _context.SaveChangesAsync();

            var result = new AppUserViewModel()
            {
                Id = userId,
                Name = isUserExist.Name,
                Surname = isUserExist.Surname,
                UserName = isUserExist.UserName,
                BirthDate = isUserExist.BirthDate
            };

            return Response<AppUserViewModel>.Success(result, 200);
        }
        public async Task<Response<List<AppUserViewModel>>> GetAllUsers()
        {
            var usersWithRoles = await _context.Users
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

            return Response<List<AppUserViewModel>>.Success(usersWithRoles, 200);
        }
        public async Task<Response<NoDataViewModel>> ApproveUsersAccount(string userId)
        {
            var isUserExist = await _userManager.FindByIdAsync(userId);
            if (isUserExist == null)
            {
                return Response<NoDataViewModel>.Fail("There is no user matched given id.", 404, true);
            }

            isUserExist.IsSubscriber = true;
            await _context.SaveChangesAsync();

            return Response<NoDataViewModel>.Success(204);
        }
        public async Task<Response<NoDataViewModel>> UpdateUser(AppUserUpdateRequestModel request)
        {
            var isUserExist = await _userManager.FindByIdAsync(request.Id);
            if (isUserExist == null)
            {
                return Response<NoDataViewModel>.Fail("There is user matched given id", 404, true);
            }

            isUserExist.Name = request.Name;
            isUserExist.Surname = request.Surname;
            isUserExist.Phone = request.Phone;
            isUserExist.BirthDate = request.BirthDate;
            isUserExist.HomeLand = request.HomeLand;

            _context.Update(isUserExist);
            await _context.SaveChangesAsync();

            return Response<NoDataViewModel>.Success(204);



        }
        public async Task<Response<AppUserViewModel>> GetSingleUserById(string userId)
        {
            var isUserExist =await _context.Users.Where(_ => _.Id == userId).Select(_ => new AppUserViewModel()
            {
                Id = _.Id,
                Name = _.Name,
                Surname = _.Surname,
                Phone = _.Phone,
                Image = _.Image,
                HomeLand = _.HomeLand,
                Email = _.Email,
                BirthDate = _.BirthDate,
                IsSubcriber = _.IsSubscriber,
                UserName = _.UserName
            }).FirstOrDefaultAsync();

            if(isUserExist == null)
            {
                return Response<AppUserViewModel>.Fail("there is no user matched given id.", 404, true);
            }

            return Response<AppUserViewModel>.Success(isUserExist, 200);
            
        }
    }
}
