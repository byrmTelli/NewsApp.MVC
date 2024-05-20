using NewsApp.CORE.DataAccess;
using NewsApp.CORE.DBModels;
using NewsApp.CORE.Generics;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.DAL.Abstract
{
    public interface IPostDal:IEntityRepository<Post>
    {
        Task<List<PostViewModel>> GetAllActivePosts();
        Task<List<PostViewModel>> GetPostsByCategory(string categoryName);
        Task<List<PostViewModel>> GetUnPublishedPostsByCategory(string categoryName);
        Task<Response<ManageSingleUserViewModel>> GetAllPostsOfUser(string userId);
    }
}
