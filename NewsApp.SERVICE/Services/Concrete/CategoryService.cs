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
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryDal _categoryDal;
        public CategoryService(
            AppDbContext appDbContext,
            UserManager<AppUser> userManager,
            ICategoryDal categoryDal
            )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _categoryDal = categoryDal;
        }



        public async Task<Response<NoDataViewModel>> ResetUsersCategoryAndRole(string userId)
        {
            var result = await _categoryDal.ResetUsersCategory(userId);
            return result;
        }
        public async Task<Response<NoDataViewModel>> CreateCategory(CategoryRequestModel request)
        {
            var isCategoryExist = await _appDbContext.Categories.Where(_ => _.Name.ToLower() == request.Name.ToLower()).FirstOrDefaultAsync();
            if(isCategoryExist != null)
            {
                return Response<NoDataViewModel>.Fail("Bu kategori zaten mevcut", 404, true);
            }

            var newCategory = new Category()
            {
                Name = request.Name
            };

            _appDbContext.Categories.Add(newCategory);

            await _appDbContext.SaveChangesAsync();
            
            return Response<NoDataViewModel>.Success(201);
        }

        public async Task<Response<List<CategoryViewModel>>> GetAllCategories()
        {
            var allCategories = await _categoryDal.GetAllCategories();

            return allCategories;
        }

        public async Task<CategoryViewModel> GetCategoryById(string id)
        {
            var isCategoryExist = await  _appDbContext.Categories.Where(_ => _.Id.ToString() == id.ToUpper()).FirstOrDefaultAsync();
            // buraya kontrol ekle


            var categoryViewModel = new CategoryViewModel()
            {
                Id = isCategoryExist.Id.ToString(),
                Name = isCategoryExist.Name

            };

            return categoryViewModel;

        }

        public async Task<CategoryViewModel> GetUsersCategory(string userId)
        {
            var result =await _appDbContext.UserCategories
                .Include(uc => uc.Category)
                .Where(uc => uc.UserId == userId)
                .Select( _ => new CategoryViewModel()
                {
                    Id = _.Category.Id.ToString(),
                    Name = _.Category.Name
                }).FirstOrDefaultAsync();

            return result;
        }
    }
}
