using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.DAL.Abstract;
using NewsApp.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Concrete
{
    public class ApprovePostDal : EfEntityRepositoryBase<PostApproveRecord, AppDbContext>, IApprovePostDal
    {
        public async Task<Response<NoDataViewModel>> ApprovePostAsync(string postId,string userId)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isPostExist = await (from post in context.Posts
                                            where post.Id.ToString() == postId
                                            select post
                                             ).FirstOrDefaultAsync();
                    if (isPostExist == null)
                    {
                        return Response<NoDataViewModel>.Fail("İlgili kayıt bulunamadı.", 404, true);
                    }

                    var newRecord = new PostApproveRecord()
                    {
                        Id = isPostExist.Id,
                        UserId =  userId,
                        PostId = postId,
                        ApprovalDate = DateTime.Now
                    };
                    context.PostApproveRecords.Add(newRecord);
                    isPostExist.IsPublished = true;
                    await context.SaveChangesAsync();

                    return Response<NoDataViewModel>.Success(204);

                }
                catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Bir hata ile karşılaşıldı.Hata: " + ex, 500, true);
                }
            }
        }

        public async Task<Response<List<ApprovePostViewModel>>> GetListOfApprovePosts()
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var result = await (from approvePost in context.PostApproveRecords
                                        join user in context.Users on approvePost.UserId equals user.Id
                                        join post in context.Posts on approvePost.PostId equals post.Id.ToString()
                                        join category in context.Categories on post.CategoryId equals category.Id
                                        select new ApprovePostViewModel()
                                        {
                                            ApproverName = user.Name,
                                            ApproverSurname = user.Surname,
                                            ApprovalDate = approvePost.ApprovalDate,
                                            Title = post.Title,
                                            CategoryName = category.Name,
                                            PostId = post.Id.ToString(),
                                        }
                                        ).ToListAsync();

                    if(result == null)
                    {
                        return Response<List<ApprovePostViewModel>>.Fail("Kayıt bulunamadı.", 404,true);
                    }

                    return Response<List<ApprovePostViewModel>>.Success(result, 200);


                }catch(Exception ex)
                {
                    return Response<List<ApprovePostViewModel>>.Fail("Bir hata meydana geldi. Hata: "+ex, 500, true);   
                }
            }
        }
    }
}
