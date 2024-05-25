using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface IApprovePostDal:IEntityRepository<PostApproveRecord>
    {
        Task<Response<NoDataViewModel>> ApprovePostAsync(string postId,string userId);
        Task<Response<List<ApprovePostViewModel>>> GetListOfApprovePosts();
    }
}
