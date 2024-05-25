using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Concrete
{
    public class CategoryDal : EfEntityRepositoryBase<Category, AppDbContext>, ICategoryDal
    {
        public async Task<Response<NoDataViewModel>> RemoveCategory(string categoryId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Categories.Where(x => x.Id.ToString() == categoryId).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.IsDeleted = true;
                        await context.SaveChangesAsync();
                        return Response<NoDataViewModel>.Success(200);
                    }
                    else
                    {
                        return Response<NoDataViewModel>.Fail(new ErrorViewModel("Kategori bulunamadı.",true), 404);
                    }
                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail(new ErrorViewModel("Bir hata meydana geldi. Hata: " + ex.Message, true), 500);
                }
            }
        }


        public async Task<Response<NoDataViewModel>> ActiveCategory(string categoryId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await context.Categories.Where(x => x.Id.ToString() == categoryId).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.IsDeleted = false;
                        await context.SaveChangesAsync();
                        return Response<NoDataViewModel>.Success(200);
                    }
                    else
                    {
                        return Response<NoDataViewModel>.Fail(new ErrorViewModel("Kategori bulunamadı.", true), 404);
                    }
                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail(new ErrorViewModel("Bir hata meydana geldi. Hata: " + ex.Message, true), 500);
                }
            }
        }

        public async Task<Response<List<CategoryViewModel>>> GetAllCategories()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await (from category in context.Categories
                                  select new CategoryViewModel()
                                  {
                                      Id =category.Id.ToString(),
                                      Name=category.Name,
                                      IsDeleted =category.IsDeleted
                                  }).ToListAsync();

                    return Response<List<CategoryViewModel>>.Success(result,200);

                }catch(Exception ex)
                {
                    return Response<List<CategoryViewModel>>.Fail(new ErrorViewModel(ex.Message, true), 500);
                }
            }
        }

        public async Task<Response<NoDataViewModel>> ResetUsersCategory(string userId)
        {
            using (var context =new AppDbContext())
            {
                try
                {

                    var userResult = await (from user in context.Users
                                            where user.Id == userId
                                            select user
                                            ).FirstOrDefaultAsync();

                    if(userResult != null) {
                        var categoryResult = await (from userCategory in context.UserCategories
                                                    where userCategory.UserId == userId
                                                    select userCategory).FirstOrDefaultAsync();

                        userResult.UserCategoryId = null;
                        context.Remove(categoryResult);
                        await context.SaveChangesAsync();


                        return Response<NoDataViewModel>.Success(200);
                    }

                    return Response<NoDataViewModel>.Fail("İlgili kayıt bulunamadı.", 404, true);

                }catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi.Hata: " + ex, 500, true);
                }
            }
        }
    }
}
