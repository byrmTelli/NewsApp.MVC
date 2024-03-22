using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IAppRoleService
    {
        Task<Response<List<AppRoleViewModel>>> GetAllRoles();
    }
}
