using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.AdminRequestModels;
using NewsApp.CORE.RequestModels.UserRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Concrete;
using NewsApp.DAL.Context;
using NewsApp.SERVICE.Services.Abstract;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IAppUserDal _userDal;
        private readonly IApproveUserDal _approveUserDal;
        public AppUserService(UserManager<AppUser> userManager, AppDbContext context, IApproveUserDal approveUserDal, IAppUserDal userDal)
        {
            _userDal = userDal;
            _userManager = userManager;
            _context = context;
            _approveUserDal = approveUserDal;
        }
        public async Task<Response<AppUserViewModel>> AssignCategoryToUser(AssingCategoryOrRoleToUserViewModel request)
        {
            var result = await _userDal.AssignCategoryToUserAsync(request);
            return result;
        }
        public async Task<Response<List<AppUserViewModel>>> GetAllUsers()
        {

            var users = await _userDal.GetAllUsersWithCategoryAndRole();

            return Response<List<AppUserViewModel>>.Success(users,200);
        }

        public async Task<Response<NoDataViewModel>> ApproveUsersAccount(ApproveUserRequestModel request)
        {
            var result = _approveUserDal.ApproveUserAsync(request);
            return Response<NoDataViewModel>.Success(204);
        }
        public async Task<Response<NoDataViewModel>> UpdateUser(AppUserUpdateRequestModel request)
        {
            var isUserExist = await _userManager.FindByIdAsync(request.Id);
            if (isUserExist == null)
            {
                return Response<NoDataViewModel>.Fail("There is user matched given id", 404, true);
            }

            if(request.Image != null)
            {
                byte[] pictureBytes;
                using (var memoryStream = new MemoryStream()) 
                {
                    await request.Image.CopyToAsync(memoryStream);
                    pictureBytes = memoryStream.ToArray();
                    isUserExist.Image = pictureBytes;
                }
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
                Image = _.Image == null ? null : "data:image/jpg;base64," + Convert.ToBase64String(_.Image),
                HomeLand = _.HomeLand,
                Email = _.Email,
                BirthDate = _.BirthDate,
                IsSubcriber = _.IsSubscriber,
                UserName = _.UserName,
                UserCategory = _context.UserCategories.Where(x => x.UserId == _.Id).Select(x => new CategoryViewModel()
                {
                    Id = x.Category.Id.ToString(),
                    Name = x.Category.Name
                }).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            if(isUserExist == null)
            {
                return Response<AppUserViewModel>.Fail("there is no user matched given id.", 404, true);
            }

            return Response<AppUserViewModel>.Success(isUserExist, 200);
            
        }
        public async Task<Response<List<AppUserViewModel>>> GetDirectorsOfCategory(string categoryId)
        {
            var category = _context.Categories.Where(_ => _.Id.ToString() == categoryId).FirstOrDefault();
            var crossList = _context.UserCategories.Where(_ => _.CategoryId == Guid.Parse(categoryId)).ToList();
            var directors = await _userManager.GetUsersInRoleAsync("director");
            var departmentDirectors = directors.Where(_ => crossList.Any(x => x.UserId == _.Id)).Select(_ => new AppUserViewModel()
            {
                Id = _.Id,
                Name = _.Name,
                Surname = _.Surname,
                UserName = _.UserName,
                Email = _.Email,
                Phone = _.Phone,
                Image = _.Image == null ? null :"data:image/jpg;base64," + Convert.ToBase64String(_.Image),
            }).ToList();
            return Response<List<AppUserViewModel>>.Success(departmentDirectors, 200);
        }
        public async Task<Response<List<AppUserViewModel>>> GetAuthorsOfCategory(string categoryId)
        {
            var department = _context.Categories.Where(_ => _.Id.ToString() == categoryId).FirstOrDefault();
            var crossList = _context.UserCategories.Where(_ => _.CategoryId == Guid.Parse(categoryId)).ToList();
            var authors = _userManager.GetUsersInRoleAsync("writer");
            var departmentAuthors = authors.Result.Where(_ => crossList.Any(x => x.UserId == _.Id)).Select(_ => new AppUserViewModel()
            {
                Id = _.Id,
                Name = _.Name,
                Surname = _.Surname,
                UserName = _.UserName,
                Email = _.Email,
                Phone = _.Phone,
                Image = _.Image == null ? null : "data:image/jpg;base64," + Convert.ToBase64String(_.Image),
            }).ToList();
            return Response<List<AppUserViewModel>>.Success(departmentAuthors, 200);
        }

        public async Task<Response<List<AppUserViewModel>>> GetAllUsersWithLinq()
        {
            try
            {
                var result = await _userDal.GetAllUsersWithCategoryAndRole();
                return Response<List<AppUserViewModel>>.Success(result, 200);
            }catch(Exception ex)
            {
                return Response<List<AppUserViewModel>>.Fail("Bir hata ile karşılaşıldı. Hata: " + ex, 500, true);
            }
        }
    }
}
