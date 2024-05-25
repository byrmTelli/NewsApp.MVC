using NewsApp.CORE.Generics;
using NewsApp.CORE.RequestModels.NewsRequestModels;
using NewsApp.CORE.ViewModels.AdminPageViewModels;
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
        Task<Response<NoDataViewModel>> Delete(string postId);
        Task<Response<NoDataViewModel>> DeletePermanently(string postId);
        Task<Response<List<PostViewModel>>> GettAllPosts();
        Task<Response<PostViewModel>> GetSinglePostById(string id);
        Task<Response<List<PostViewModel>>> FilterPost(Func<PostViewModel,bool> exp);
        Task<Response<NoDataViewModel>> Create(PostRequestModel model);
        Task<Response<ManageSingleUserViewModel>> GetAllPostsOfUser(string userId);
        Task<Response<List<PostViewModel>>> GetUnPublishedPostsByCategory(string categoryName);
        Task<Response<NoDataViewModel>> ApprovePost(string postId);
        Task<Response<NoDataViewModel>> UpdatePost(PostRequestModel model);
    }
}
