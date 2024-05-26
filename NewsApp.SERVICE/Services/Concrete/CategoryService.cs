using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.CategoyRequestModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Context;
using NewsApp.SERVICE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryService(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }



        public async Task<Response<NoDataViewModel>> ResetUsersCategoryAndRole(string userId)
        {
            var result = await _categoryDal.ResetUsersCategory(userId);
            return result;
        }
        public async Task<Response<NoDataViewModel>> CreateCategory(CategoryRequestModel request)
        {
            var result = await _categoryDal.CreateCategory(request);
            return result;
        }
        public async Task<Response<List<CategoryViewModel>>> GetAllCategories()
        {
            var allCategories = await _categoryDal.GetAllCategories();

            return allCategories;
        }

        public async Task<CategoryViewModel> GetCategoryById(string id)
        {
            var result =await  _categoryDal.GetCategoryById(id);
            return result;

        }

        public async Task<CategoryViewModel> GetUsersCategory(string userId)
        {
            var result = await _categoryDal.GetUsersCategory(userId);
            return result;
        }
    }
}
