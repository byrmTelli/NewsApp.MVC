using NewsApp.CORE.RequestModels.CategoyRequestModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.PageViewModels.ManageDepartmentViewModels
{
    public class ManageDepartmentsPageViewModel
    {
        public List<CategoryViewModel>? CategoryViewModel { get; set; }
        public CategoryRequestModel CategoryRequestModel { get; set; }
    }
}
