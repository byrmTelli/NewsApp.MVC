using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.UserViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Concrete
{
    public class AppUserDal:EfEntityRepositoryBase<AppUser,AppDbContext>, IAppUserDal
    {
        public async Task<NoDataViewModel> DeleteUser(string id)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isUserExist =await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

                    if (isUserExist == null)
                    {
                        return new NoDataViewModel();
                    }

                    isUserExist.IsDeleted = true;
                    await context.SaveChangesAsync();
                    return new NoDataViewModel();

                }catch (Exception ex)
                {
                    return new NoDataViewModel();
                }
            }
        }

        public async Task<List<AppUserViewModel>> GetAllUsersWithCategoryAndRole()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = (from user in context.Users
                                  join userRole in context.UserRoles on user.Id equals userRole.UserId
                                  join role in context.Roles on userRole.RoleId equals role.Id
                                  join userCategory in context.UserCategories on user.Id equals userCategory.UserId into userCategoryJoin
                                  from userCategory in userCategoryJoin.DefaultIfEmpty()
                                  join category in context.Categories on userCategory.CategoryId equals category.Id into categoryJoin
                                  from category in categoryJoin.DefaultIfEmpty()
                                  select new AppUserViewModel()
                                  {
                                      Id = user.Id,
                                      Name = user.Name,
                                      Surname = user.Surname,
                                      BirthDate = user.BirthDate,
                                      Email = user.Email,
                                      HomeLand = user.HomeLand,
                                      Roles = new List<string>() { role.Name },
                                      UserCategory = category != null ? new CategoryViewModel() { Id = category.Id.ToString(), Name = category.Name } : null
                                  }).ToListAsync();

                    return await result;
                }
                catch (Exception ex)
                {
                    // Hata mesajını loglayabiliriz.
                    Console.WriteLine(ex.Message);
                    return new List<AppUserViewModel>();
                }
            }
        }

        public async Task<NoDataViewModel> ReActiveUser(string id)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isUserExist = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

                    if (isUserExist == null)
                    {
                        return new NoDataViewModel();
                    }

                    isUserExist.IsDeleted = false;
                    await context.SaveChangesAsync();
                    return new NoDataViewModel();

                }
                catch (Exception ex)
                {
                    return new NoDataViewModel();
                }
            }
        }
    }
}
