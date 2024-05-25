using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.NewsRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
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
        public async Task<int> GetMontlyApprovedPostCount()
        {
            using(var context = new AppDbContext())
            {
                try
                {

                    var result = await (from post in context.Posts
                                        where post.CreatedAt.Month == DateTime.Now.Month
                                        select post
                                       ).ToListAsync();
                    if(result == null)
                    {
                        return 0;
                    }

                    return result.Count;

                }catch(Exception ex)
                {
                    return 0;
                }
            }
        }
        public async Task<Response<NoDataViewModel>> Delete(string postId)
        {
            using (var context  = new AppDbContext())
            {
                try
                {

                    var isPostExist = await (from post in context.Posts
                                             where post.Id == Guid.Parse(postId)
                                             select post
                                            ).FirstOrDefaultAsync();
                    if(isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no post matched given id", 404, true);
                    }

                    isPostExist.IsDeleted = true;
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(201);

                }catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi.Hata: " + ex, 500,true);
                }
            }
        }
        public async Task<Response<NoDataViewModel>> DeletePermanently(string postId)
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var isPostExist = await(from post in context.Posts
                                            where post.Id == Guid.Parse(postId)
                                            select post
                                            ).FirstOrDefaultAsync();
                    if (isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("There is no post matched given id", 404, true);
                    }

                    context.Remove(isPostExist);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(201);

                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi.Hata: " + ex, 500, true);
                }
            }
        }
        public async Task<Response<PostViewModel>> GetPostById(string postId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isPostExist = await (from post in context.Posts
                                             join postUser in context.Users on post.CreatorId equals postUser.Id
                                             join category in context.Categories on post.CategoryId equals category.Id
                                             where  post.CategoryId == category.Id && post.Id.ToString() == postId
                                             select new PostViewModel()
                                             {
                                                 Id = post.Id.ToString(),
                                                 Title = post.Title,
                                                 Content = post.Content,
                                                 CreatedAt =post.CreatedAt,
                                                 IsSubscriberOnly = post.IsPrivateOnly,
                                                 Image = post.Image,
                                                 Creator = new AppUserViewModel()
                                                 {
                                                     Id=postUser.Id,
                                                     Name = postUser.Name,
                                                     Surname = postUser.Surname,
                                                     UserName = postUser.UserName,
                                                     UserCategory = new CategoryViewModel()
                                                     {
                                                         Id = category.Id.ToString(),
                                                         Name = category.Name,
                                                         IsDeleted = category.IsDeleted,
                                                     }
                                                 },
                                                 Category = new CategoryViewModel()
                                                 {
                                                     Id = category.Id.ToString(),
                                                     Name = category.Name,
                                                     IsDeleted = category.IsDeleted,
                                                 },
                                                 IsDeleted = post.IsDeleted,
                                                 IsPublished = post.IsPublished
                                             }).FirstOrDefaultAsync();

                    if(isPostExist == null)
                    {
                        return Response<PostViewModel>.Fail("İlgili post bulunamadı.",404, true);
                    }

                    return Response<PostViewModel>.Success(isPostExist, 200);


                }catch(Exception ex)
                {
                    return Response<PostViewModel>.Fail("Bir hata meydana geldi.Hata: ", 500, true);
                }
            }
        }
        public async Task<Response<NoDataViewModel>> ApprovePost(string postId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isPostExist = await (from post in context.Posts
                                             where post.Id.ToString() == postId
                                             select post
                                             ).FirstOrDefaultAsync();
                    if(isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("İlgili kayıt bulunamadı.", 404, true);
                    }

                    isPostExist.IsPublished = true;
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(204);

                }catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata ile karşılaşıldı.Hata: " + ex, 500, true);
                }
            }
        }
        public async Task<Response<NoDataViewModel>> Create(PostRequestModel request)
        {
            using(var context = new AppDbContext())
            {
                try
                {
                    var newsModel = new Post()
                    {
                        Title = request.Title,
                        CategoryId = Guid.Parse(request.CategoryId),
                        Content = request.Content,
                        CreatorId = request.CreatorId,
                        CreatedAt = DateTime.Now,
                        Image = request.Image,
                        IsPrivateOnly = request.IsPrivateOnly,
                        IsPublished = request.IsPublished
                    };

                    context.Posts.Add(newsModel);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(204);
                }
                catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi.Hata: " + ex, 500, true);
                }
            }
        }

        public async Task<Response<NoDataViewModel>> Update(PostRequestModel request)
        {
            using(var context = new AppDbContext())
            {
                try
                {

                    var isPostExist = await (from post in context.Posts
                                             where post.Id.ToString() == request.Id
                                             select new Post()
                                             {
                                                 Id= Guid.Parse(request.Id),
                                                 Content = request.Content,
                                                 CreatedAt = post.CreatedAt,
                                                 Image = request.Image,
                                                 IsPrivateOnly = request.IsPrivateOnly,
                                                 IsPublished = false,
                                                 IsDeleted = post.IsDeleted,
                                                 CreatorId = post.CreatorId,
                                                 Creator = post.Creator,
                                                 Category=post.Category,
                                                 CategoryId=post.CategoryId,
                                                 Title = request.Title
                                             }
                                             ).FirstOrDefaultAsync();

                    if(isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("Kayıt bulunamadı.", 404,true);
                    }

                    context.Update(isPostExist);
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(204);

                }catch(Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata meydana geldi. Hata: " + ex, 500, true);
                }
            }
        }
    }
}
