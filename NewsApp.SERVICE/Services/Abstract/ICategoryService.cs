using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.CategoyRequestModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryViewModel>>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(string id);
        Task<CategoryViewModel> GetUsersCategory(string id);
        Task<Response<NoDataViewModel>> CreateCategory(CategoryRequestModel request);
        Task<Response<NoDataViewModel>> ResetUsersCategoryAndRole(string userId);
    }
}
