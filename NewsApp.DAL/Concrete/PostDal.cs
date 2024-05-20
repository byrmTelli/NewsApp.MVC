using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
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
    public class PostDal : EfEntityRepositoryBase<Post, AppDbContext>, IPostDal
    {
        public async Task<List<PostViewModel>> GetAllActivePosts()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var result = await (from activePost in context.Posts
                                 join postCategory in context.Categories on activePost.CategoryId equals postCategory.Id
                                 where activePost.IsDeleted == false 
                                 && activePost.Creator.IsDeleted == false 
                                 && activePost.IsPublished == true 
                                 && postCategory.IsDeleted ==false
                                 select new PostViewModel()
                                 {
                                     Id = activePost.Id.ToString(),
                                     Title = activePost.Title,
                                     Content = activePost.Content,
                                     CreatedAt = activePost.CreatedAt,
                                     IsSubscriberOnly = activePost.IsPrivateOnly,
                                     Image = activePost.Image,
                                     Creator = new AppUserViewModel()
                                     {
                                         Id=activePost.Creator.Id.ToString(),
                                         Name = activePost.Creator.Name,
                                         Surname = activePost.Creator.Surname,
                                         Phone = activePost.Creator.Phone,
                                     },
                                     Category = new CategoryViewModel()
                                     {
                                         Id = postCategory.Id.ToString(),
                                         Name =postCategory.Name
                                     } 
                                 }
                                 ).ToListAsync();

                    return result;


                }catch (Exception ex)
                {
                    return new List<PostViewModel>();
                }
            }
        }
        public async Task<List<PostViewModel>> GetPostsByCategory(string categoryName)
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var result = await (from activePost in context.Posts
                           join postCategory in context.Categories on activePost.CategoryId equals postCategory.Id
                           where activePost.IsDeleted == false 
                           && activePost.Creator.IsDeleted == false 
                           && activePost.IsPublished == true 
                           && postCategory.Name == categoryName
                           && activePost.Category.IsDeleted == false
                           select new PostViewModel()
                           {
                               Id = activePost.Id.ToString(),
                               Title = activePost.Title,
                               Content = activePost.Content,
                               CreatedAt = activePost.CreatedAt,
                               IsSubscriberOnly = activePost.IsPrivateOnly,
                               Image = activePost.Image,
                               Creator = new AppUserViewModel()
                               {
                                   Id = activePost.Creator.Id.ToString(),
                                   Name = activePost.Creator.Name,
                                   Surname = activePost.Creator.Surname,
                                   Phone = activePost.Creator.Phone,
                               },
                               Category = new CategoryViewModel()
                               {
                                   Id = postCategory.Id.ToString(),
                                   Name = postCategory.Name
                               }
                           }).ToListAsync();

                    return result;


                }
                catch(Exception ex)
                {
                    return new List<PostViewModel>();
                }
            }
        }
        public async Task<List<PostViewModel>> GetUnPublishedPostsByCategory(string categoryName)
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var result = await(from activePost in context.Posts
                                       join postCategory in context.Categories on activePost.CategoryId equals postCategory.Id
                                       where activePost.IsDeleted == false && activePost.Creator.IsDeleted == false && activePost.IsPublished == false && postCategory.Name == categoryName
                                       select new PostViewModel()
                                       {
                                           Id = activePost.Id.ToString(),
                                           Title = activePost.Title,
                                           Content = activePost.Content,
                                           CreatedAt = activePost.CreatedAt,
                                           IsSubscriberOnly = activePost.IsPrivateOnly,
                                           Image = activePost.Image,
                                           Creator = new AppUserViewModel()
                                           {
                                               Id = activePost.Creator.Id.ToString(),
                                               Name = activePost.Creator.Name,
                                               Surname = activePost.Creator.Surname,
                                               Phone = activePost.Creator.Phone,
                                           },
                                           Category = new CategoryViewModel()
                                           {
                                               Id = postCategory.Id.ToString(),
                                               Name = postCategory.Name
                                           }
                                       }).ToListAsync();

                    return result;


                }
                catch (Exception ex)
                {
                    return new List<PostViewModel>();
                }
            }
        }

        public async Task<Response<ManageSingleUserViewModel>> GetAllPostsOfUser(string userId)
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var posts = await (from activePost in context.Posts
                           join postCategory in context.Categories on activePost.CategoryId equals postCategory.Id
                           where activePost.CreatorId == userId
                           select new PostViewModel()
                           {
                               Id = activePost.Id.ToString(),
                               Title = activePost.Title,
                               Content = activePost.Content,
                               CreatedAt = activePost.CreatedAt,
                               IsSubscriberOnly = activePost.IsPrivateOnly,
                               Image = activePost.Image,
                               IsDeleted = activePost.IsDeleted,
                               IsPublished = activePost.IsPublished,
                               Creator = new AppUserViewModel()
                               {
                                   Id = activePost.Creator.Id.ToString(),
                                   Name = activePost.Creator.Name,
                                   Surname = activePost.Creator.Surname,
                                   Phone = activePost.Creator.Phone,
                               },
                               Category = new CategoryViewModel()
                               {
                                   Id = postCategory.Id.ToString(),
                                   Name = postCategory.Name
                               }
                           }).ToListAsync();


                    var user =await (from appUser in context.Users
                                    join userRole in context.UserRoles on appUser.Id equals userRole.UserId
                                    join role in context.Roles on userRole.RoleId equals role.Id
                                    join userCategory in context.UserCategories on appUser.Id equals userCategory.UserId into userCategoryJoin
                                    from userCategory in userCategoryJoin.DefaultIfEmpty()
                                    join category in context.Categories on userCategory.CategoryId equals category.Id into categoryJoin
                                    from category in categoryJoin.DefaultIfEmpty()
                                    where appUser.Id == userId
                                     select new AppUserViewModel()
                                    {
                                        Id = appUser.Id,
                                        Name = appUser.Name,
                                        Surname = appUser.Surname,
                                        BirthDate = appUser.BirthDate,
                                        Email = appUser.Email,
                                        HomeLand = appUser.HomeLand,
                                        IsDeleted =appUser.IsDeleted,
                                        Roles = new List<string>() { role.Name },
                                        UserCategory = category != null ? new CategoryViewModel() { Id = category.Id.ToString(), Name = category.Name } : null
                                    }).FirstOrDefaultAsync();



                    var result = new ManageSingleUserViewModel()
                    {
                        User = user,
                        Posts = posts
                    };
                    return Response<ManageSingleUserViewModel>.Success(result,200);


                }
                catch(Exception ex)
                {
                    return Response<ManageSingleUserViewModel>.Fail("Bir hata ile karşılaşıldı. Hata: "+ex,500,true);
                }
            }
        }
    }
}
