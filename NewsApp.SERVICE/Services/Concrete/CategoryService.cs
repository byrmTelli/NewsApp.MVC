using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
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
        public CategoryService(AppDbContext appDbContext,UserManager<AppUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var allCategories = await _appDbContext.Categories
                .Select(_ => new CategoryViewModel()
                {
                    Id = _.Id.ToString(),
                    Name = _.Name
                })
                .ToListAsync();

            return allCategories;
        }

        public async Task<CategoryViewModel> GetCategoryById(string id)
        {
            var isCategoryExist = await  _appDbContext.Categories.Where(_ => _.Id.ToString() == id).FirstOrDefaultAsync();
            // buraya kontrol ekle


            var categoryViewModel = new CategoryViewModel()
            {
                Id = isCategoryExist.Id.ToString(),
                Name = isCategoryExist.Name

            };

            return categoryViewModel;

        }

        public async Task<List<CategoryViewModel>> GetUsersCategory(string userId)
        {
            var result =await _appDbContext.UserCategories
                .Include(uc => uc.Category)
                .Where(uc => uc.UserId == userId)
                .Select( _ => new CategoryViewModel()
                {
                    Id = _.Category.Id.ToString(),
                    Name = _.Category.Name
                }).ToListAsync();

            return result;
        }
    }
}
