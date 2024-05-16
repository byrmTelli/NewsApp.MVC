using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.NewsRequestModels;
using NewsApp.CORE.ViewModels.CustomViewModels;
using NewsApp.CORE.ViewModels.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IPostService
    {
        Task<Response<List<PostViewModel>>> GetPostsByCategory(string categoryName);
        Task<Response<NoDataViewModel>> Delete(string id);
        Task<Response<List<PostViewModel>>> GettAllPosts();
        Task<PostViewModel> GetSingleNewsById(string id);
        Task<Response<List<PostViewModel>>> FilterPost(Func<PostViewModel,bool> exp);
        Task<Response<NoDataViewModel>> Create(PostRequestModel model);
        Task<Response<List<PostViewModel>>> GetAllPostsOfUser(string userId);
        Task<Response<List<PostViewModel>>> GetUnPublishedPostsByCategory(string categoryName);
        Task<Response<NoDataViewModel>> ApprovePost(string postId);
    }
}
