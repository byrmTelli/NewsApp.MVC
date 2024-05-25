using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels;
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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AppUserDal(
            RoleManager<AppRole> roleManager,
            UserManager<AppUser> userManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public async Task<Response<AppUserViewModel>> AssignCategoryToUserAsync(AssingCategoryOrRoleToUserViewModel request)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var isUserExist = await (from user in context.Users
                                             where user.Id == request.UserId
                                             select user).FirstOrDefaultAsync();

                    if (isUserExist == null)
                    {
                        return Response<AppUserViewModel>.Fail("Böyle bir kullanıcı bulunamadı.", 404, true);
                    }

                    if (request.CategoryId != null)
                    {
                        var oldCategory = await (from categoryRecord in context.UserCategories
                                                 where categoryRecord.UserId == request.UserId
                                                 select categoryRecord).FirstOrDefaultAsync();

                        if (oldCategory != null)
                        {
                            context.Remove(oldCategory);
                        }

                        var isCategoryExist = await (from category in context.Categories
                                                     where category.Id == Guid.Parse(request.CategoryId)
                                                     select category).FirstOrDefaultAsync();

                        if (isCategoryExist == null)
                        {
                            return Response<AppUserViewModel>.Fail("Böyle bir kategori bulunamadı.", 404, true);
                        }

                        context.UserCategories.Add(new AppUserCategory()
                        {
                            UserId = request.UserId,
                            CategoryId = Guid.Parse(request.CategoryId)
                        });
                        isUserExist.UserCategoryId = isCategoryExist.Id;
                    }

                    await context.SaveChangesAsync();
                }

                if (request.RoleId != null)
                {
                    var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                    var role = await _roleManager.FindByIdAsync(request.RoleId);

                    if (role == null)
                    {
                        return Response<AppUserViewModel>.Fail("Böyle bir rol bulunamadı.", 404, true);
                    }

                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(user, userRoles);
                    }

                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                return Response<AppUserViewModel>.Success(200);
            }
            catch (Exception ex)
            {
                return Response<AppUserViewModel>.Fail("Bir Hata Meydana geldi. Hata: " + ex, 500, true);
            }
        }


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
                    var result = await (from user in context.Users
                                        join userRole in context.UserRoles on user.Id equals userRole.UserId into userRoleJoin
                                        from userRole in userRoleJoin.DefaultIfEmpty()
                                        join role in context.Roles on userRole.RoleId equals role.Id into roleJoin
                                        from role in roleJoin.DefaultIfEmpty()
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
                                            IsSubcriber = user.IsSubscriber,
                                            IsDeleted = user.IsDeleted,
                                            Phone = user.Phone,
                                            Roles = role != null ? new List<string>() { role.Name } : new List<string>(),
                                            UserCategory = category != null ? new CategoryViewModel() { Id = category.Id.ToString(), Name = category.Name } : null
                                        }).ToListAsync();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new List<AppUserViewModel>();
                }
            }
        }


        public async Task<int> NewUserCountMontly()
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var result = await (from user in context.Users
                                  where user.CreatedDate.Month == DateTime.Now.Month && user.IsDeleted == false && user.IsSubscriber == true
                                  select new AppUserViewModel()).ToListAsync();
                    if(result == null)
                    {
                        return 0;
                    }

                    return result.Count();
                    


                }catch(Exception ex)
                {
                    return 0;
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
