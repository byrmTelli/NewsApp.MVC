using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.RoleViewModels;
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
    public class AppRoleService : IAppRoleService
    {
        private readonly IUserRoleDal _roleDal;
        public AppRoleService(IUserRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public async Task<Response<List<AppRoleViewModel>>> GetAllRoles()
        {
            var allRoles  =await _roleDal.GetListOfRoles();

            return allRoles;
        }
    }
}
