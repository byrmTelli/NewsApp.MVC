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
        private readonly AppDbContext _context;
        private readonly IPostDal _postDal;

        public PostService(AppDbContext context,IPostDal postDal)
        {
            _context = context;
            _postDal = postDal;
        }

        
        public async Task<Response<NoDataViewModel>>Delete(string id)
        {
            var isPostExist = await _context.Posts.Where(_ => _.Id.ToString() == id).FirstOrDefaultAsync();
            if(isPostExist == null)
            {
                return Response<NoDataViewModel>.Fail("There is no post matched given id value", 404,true);
            }

            _context.Remove(isPostExist);
            await _context.SaveChangesAsync();

            return Response<NoDataViewModel>.Success(204);

        }
        public async Task<Response<List<PostViewModel>>> GettAllPosts()
        {
            var allNews =await _postDal.GetAllActivePosts();

            return Response<List<PostViewModel>>.Success(allNews,200);
        }
        public async Task<PostViewModel> GetSingleNewsById(string id)
        {
            var news = await _context.Posts.FirstOrDefaultAsync(_ => _.Id.ToString() == id);

            if (news == null)
            {
                throw new ArgumentNullException(nameof(news), "Haber bulunamadı.");
            }
            var category = await _context.Categories.Where(_ => _.Id == news.CategoryId).Select(_ => new CategoryViewModel() { Name = _.Name }).FirstOrDefaultAsync();
            var creator = await _context.Users.Where(_ => _.Id == news.CreatorId).Select(_ => new AppUserViewModel() {
                Id = _.Id,
                Name = _.Name,
                Surname = _.Surname,
                BirthDate = _.BirthDate
            })
                .FirstOrDefaultAsync();
            return new PostViewModel
            {
                Id = news.Id.ToString(),
                Title = news.Title,
                Creator =creator,
                Category = category,
                Content = news.Content,
                CreatedAt = news.CreatedAt,
                Image = news.Image
            };
        }
        public async Task<Response<NoDataViewModel>> Create(PostRequestModel model)
        {
            var newsModel = new Post()
            {
                Title = model.Title,
                CategoryId = Guid.Parse(model.CategoryId),
                Content = model.Content,
                CreatorId = model.CreatorId,
                CreatedAt = DateTime.Now,
                Image = model.Image,
                IsPrivateOnly = model.IsPrivateOnly,
                IsPublished = model.IsPublished
            };

            _context.Posts.Add(newsModel);
            await _context.SaveChangesAsync();

            return Response<NoDataViewModel>.Success(204);
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

        public async Task<Response<NoDataViewModel>> ApprovePost(string postId)
        {
            var isPostExist =await _context.Posts.Where(_ => _.Id.ToString() == postId).FirstOrDefaultAsync();
            if (isPostExist == null)
            {
                return Response<NoDataViewModel>.Fail("There is no post matched given id value", 404, true);
            }

            isPostExist.IsPublished = true;
            await _context.SaveChangesAsync();
            return Response<NoDataViewModel>.Success(200);
        }
    }
}
