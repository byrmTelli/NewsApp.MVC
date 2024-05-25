using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface IUserRoleDal: IEntityRepository<AppRole>
    {
        Task<Response<List<AppRoleViewModel>>> GetListOfRoles();
    }
}
