using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.RoleViewModels;
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
        private readonly AppDbContext _context;
        public AppRoleService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<AppRoleViewModel>>> GetAllRoles()
        {
            var allRoles  =await _context.Roles.Select(_ => new AppRoleViewModel()
            {
                Id = _.Id,
                RoleName = _.Name
            }).ToListAsync();

            return Response<List<AppRoleViewModel>>.Success(allRoles, 200);
        }
    }
}
