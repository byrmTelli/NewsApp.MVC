using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        Task<Response<NoDataViewModel>> RemoveCategory(string categoryId);
        Task<Response<NoDataViewModel>> ActiveCategory(string categoryId);
        Task<Response<List<CategoryViewModel>>> GetAllCategories();
    }
}
