using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data.Entities;
using Blogger.Shared;

namespace Blogger.Service
{
    public interface IUserBlogService
    {
        IEnumerable<BlogViewModel>? GetUserBlogs(string appUserId);
        IEnumerable<BlogViewModel>? GetSearchedUserBlogs(string appUserId, string searchText);
        IEnumerable<BlogViewModel>? GetUserBlogs(string appUserId, int blogPage = 1, int pageSize = 10, string? searchText = null);

        IEnumerable<BlogViewModel>? GetUserBlogs();
        IEnumerable<BlogViewModel>? GetSearchedUserBlogs(string searchText = "");
        IEnumerable<BlogViewModel>? GetUserBlogs(int blogPage = 1, int pageSize = 10, string? searchText = null);

        void Create(BlogSaveModel model);

    }
}
