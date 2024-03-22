using NewsApp.CORE.ViewModels.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(string id);
        Task<List<CategoryViewModel>> GetUsersCategory(string id);
    }
}
