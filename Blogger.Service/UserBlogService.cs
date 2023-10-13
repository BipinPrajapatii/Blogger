using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blogger.Data.Entities;
using Blogger.Repo;
using Blogger.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Service
{
    public class UserBlogService : IUserBlogService
    {
        private readonly IMapper _mapper;
        private readonly IUserBlogRepository _repository;

        public UserBlogService(IUserBlogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(BlogSaveModel model)
        {

            var blog = new UserBlog();
            blog.AppUserId = model.AppUserID;
            blog.Blog = new Blog { Content = model.Content, DatePublished = model.DatePublished, Title = model.Title };
            _repository.Create(blog);
        }

        public IEnumerable<BlogViewModel>? GetUserBlogs(string appUserId)
        {
            var blogs = _repository.GetAllByAppUserID(appUserId);
            var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
            return models;
        }

        public IEnumerable<BlogViewModel>? GetUserBlogs()
        {
            var blogs = _repository.GetAll();
            var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
            return models;
        }

        public IEnumerable<BlogViewModel>? GetUserBlogs(int blogPage = 1, int pageSize = 2, string searchText = "")
        {

            if (string.IsNullOrEmpty(searchText))
            {
                var blogs = _repository.GetAll()?.Skip((blogPage - 1) * pageSize).Take(pageSize);
                var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
                return models;
            }
            else
            {
                var blogs = _repository.GetAll()?.Where(blog => blog!.Blog!.Title!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) || blog!.Blog!.Content!.HTMLToText().Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).Skip((blogPage - 1) * pageSize).Take(pageSize);
                var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
                return models;
            }
        }

        public IEnumerable<BlogViewModel>? GetSearchedUserBlogs(string searchText = "")
        {
            searchText ??= "";
            var blogs = _repository.GetAll()?.Where(blog => blog!.Blog!.Title!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) || blog!.Blog!.Content!.HTMLToText().Contains(searchText, StringComparison.CurrentCultureIgnoreCase));
            var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
            return models;
        }

        public IEnumerable<BlogViewModel>? GetSearchedUserBlogs(string appUserId, string searchText)
        {
            searchText ??= "";
            var blogs = _repository.GetAll()?.Where(blog => blog.AppUserId == appUserId && (blog!.Blog!.Title!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) || blog!.Blog!.Content!.HTMLToText().Contains(searchText, StringComparison.CurrentCultureIgnoreCase)));
            var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
            return models;
        }

        public IEnumerable<BlogViewModel>? GetUserBlogs(string appUserId, int blogPage = 1, int pageSize = 10, string? searchText = null)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                var blogs = _repository.GetAll()?.Where(blog => blog.AppUserId == appUserId).Skip((blogPage - 1) * pageSize).Take(pageSize);
                var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
                return models;
            }
            else
            {
                var blogs = _repository.GetAll()?.Where(blog => blog!.Blog!.Title!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) || blog!.Blog!.Content!.HTMLToText().Contains(searchText, StringComparison.CurrentCultureIgnoreCase)).Skip((blogPage - 1) * pageSize).Take(pageSize);
                var models = _mapper.Map<IEnumerable<BlogViewModel>>(blogs);
                return models;
            }
        }
    }
}
