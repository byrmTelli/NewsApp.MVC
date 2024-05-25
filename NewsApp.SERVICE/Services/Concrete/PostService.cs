using Microsoft.EntityFrameworkCore;
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
using NewsApp.SERVICE.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Concrete
{
    public class PostService:IPostService
    {
        private readonly IPostDal _postDal;
        private readonly IApprovePostDal _approvePostDal;

        public PostService(IPostDal postDal, IApprovePostDal approvePostDal)
        {
            _postDal = postDal;
            _approvePostDal = approvePostDal;
        }

        
        public async Task<Response<NoDataViewModel>>Delete(string id)
        {
            var result = await _postDal.Delete(id);
            return result;
        }
        public async Task<Response<NoDataViewModel>> DeletePermanently(string postId)
        {
            var result = await _postDal.DeletePermanently(postId);
            return result;
        }
        public async Task<Response<List<PostViewModel>>> GettAllPosts()
        {
            var allNews =await _postDal.GetAllActivePosts();

            return Response<List<PostViewModel>>.Success(allNews,200);
        }
        public async Task<Response<PostViewModel>> GetSinglePostById(string postId)
        {
            var result = await _postDal.GetPostById(postId);
            return result;
        }
        public async Task<Response<NoDataViewModel>> Create(PostRequestModel model)
        {
            var result = await _postDal.Create(model);
            return result;
        }
        public async Task<Response<ManageSingleUserViewModel>> GetAllPostsOfUser(string userId)
        {
            var userPosts = await _postDal.GetAllPostsOfUser(userId);
            return userPosts;
        }
        public async Task<Response<List<PostViewModel>>> FilterPost(Func<PostViewModel, bool> exp)
        {
            throw new ArgumentException();
        }
        public async Task<Response<List<PostViewModel>>> GetPostsByCategory(string categoryName)
        {
            var posts = await  _postDal.GetPostsByCategory(categoryName);

            if (posts == null)
            {
                return Response<List<PostViewModel>>.Fail("There is no post matched given category id value", 404, true);
            }

            return Response<List<PostViewModel>>.Success(posts, 200);
        }
        public async Task<Response<List<PostViewModel>>> GetUnPublishedPostsByCategory(string categoryName)
        {
            var posts =await _postDal.GetUnPublishedPostsByCategory(categoryName);

            if (posts == null)
            {
                return Response<List<PostViewModel>>.Fail("There is no post matched given category id value", 404, true);
            }

            return Response<List<PostViewModel>>.Success(posts, 200);
        }
        public async Task<Response<NoDataViewModel>> ApprovePost(string postId,string userId)
        {
            var result = await _approvePostDal.ApprovePostAsync(postId,userId);
            return result;
        }

        public async Task<Response<NoDataViewModel>> UpdatePost(PostRequestModel model)
        {
            var result = await _postDal.Update(model);
            return result;
        }
    }
}
