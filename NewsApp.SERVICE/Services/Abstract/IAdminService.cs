using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IAdminService
    {
        Task<Response<ManageUserViewModel>> ManageUsers();
    }
}
