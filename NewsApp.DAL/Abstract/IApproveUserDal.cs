using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.AdminRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface IApproveUserDal:IEntityRepository<UserApproveRecord>
    {
        Task<Response<NoDataViewModel>> ApproveUserAsync(ApproveUserRequestModel request);
        Task<Response<List<ApproveUserViewModel>>> GetListOfApproveRecords();
    }
}
