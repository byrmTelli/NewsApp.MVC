using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.RoleViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Concrete
{
    public class UserRoleDal : EfEntityRepositoryBase<AppRole, AppDbContext>, IUserRoleDal
    {
        public async Task<Response<List<AppRoleViewModel>>> GetListOfRoles()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await (from role in context.Roles select new AppRoleViewModel()
                    {
                        Id=role.Id,
                        RoleName =role.Name
                    }).ToListAsync();
                    if(result != null)
                    {
                        return Response<List<AppRoleViewModel>>.Success(result, 200);
                    }

                    return Response<List<AppRoleViewModel>>.Fail("Kayıtlı herhangi bir role bilgisi yok.",404,true);

                }catch(Exception ex)
                {
                    return Response<List<AppRoleViewModel>>.Fail("Kayıt alınırken bir hata meydana geldi. Hata: "+ex, 500, true);
                }
            }
        }
    }
}
