using Microsoft.EntityFrameworkCore;
using NewsApp.CORE.DataAccess.EntityFramework;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.AdminRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
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
    public class ApproveUserDal : EfEntityRepositoryBase<UserApproveRecord, AppDbContext>, IApproveUserDal
    {
        public async Task<Response<NoDataViewModel>> ApproveUserAsync(ApproveUserRequestModel request)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var isUserExist = await (from user in context.Users
                                               where user.Id == request.UserId
                                               select user
                                               ).FirstOrDefaultAsync();

                    if(isUserExist != null)
                    {
                        isUserExist.IsSubscriber = true;
                        var newRecord = new UserApproveRecord()
                        {
                            ApprovedUserId = request.ApproverId,
                            UserId = request.UserId,
                            ApprovalDate = DateTime.Now,
                        };

                        context.Add(newRecord);
                        await context.SaveChangesAsync();

                        return Response<NoDataViewModel>.Success(204);
                    }

                    return Response<NoDataViewModel>.Fail("Bu kullanıcıya ait bir kayıt bulunamadı.",404,true);


                }catch (Exception ex)
                {
                    return Response<NoDataViewModel>.Fail("Kayıt ekleniken bir hata ile karşılaşıldı. Hata: "+ex,500,true);
                }
            }
        }

        public async Task<Response<List<ApproveUserViewModel>>> GetListOfApproveRecords()
        {
            using (var context = new AppDbContext())
            {
                try
                {

                    var result = await (from record in context.UserApproveRecords
                                  join approvedUser in context.Users on record.ApprovedUserId equals approvedUser.Id
                                  join user in context.Users on record.UserId equals user.Id
                                  select new ApproveUserViewModel()
                                  {
                                      ApprovalDate = record.ApprovalDate,
                                      ApproverName = approvedUser.Name,
                                      ApproverSurname = approvedUser.Surname,
                                      UserMail = user.Email
                                  }).ToListAsync();

                    if(result != null)
                    {
                        return Response<List<ApproveUserViewModel>>.Success(result, 200);
                    }

                    return Response<List<ApproveUserViewModel>>.Fail("Kayıt bulunamadı.", 404, true);

                }catch (Exception ex)
                {
                    return Response<List<ApproveUserViewModel>>.Fail("Bir hata meydana geldi.Hata: "+ex, 500, true);
                }
            }
        }
    }
}
