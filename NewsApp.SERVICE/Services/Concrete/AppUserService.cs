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

        private readonly IAppUserDal _userDal;
        private readonly IApproveUserDal _approveUserDal;
        public AppUserService(IApproveUserDal approveUserDal, IAppUserDal userDal)
        {
            _userDal = userDal;
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
            var result = await _userDal.UpdateUser(request);
            return result;
        }
        public async Task<Response<AppUserViewModel>> GetSingleUserById(string userId)
        {
            var result = await _userDal.GetSingleUserById(userId);
            return result;

            
        }
        public async Task<Response<List<AppUserViewModel>>> GetDirectorsOfCategory(string categoryId)
        {
            var result = await _userDal.GetDirectorsOfCategory(categoryId);
            return result;
        }
        public async Task<Response<List<AppUserViewModel>>> GetAuthorsOfCategory(string categoryId)
        {
            var result = await _userDal.GetAuthorsOfCategory(categoryId);
            return result;
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
