using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data;
using Blogger.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Repo
{
    public class UserBlogRepository : IUserBlogRepository
    {
        private readonly BloggerDbContext _context;

        public UserBlogRepository(BloggerDbContext context)
        {
            _context = context;
        }

        public void Create(UserBlog userBlog)
        {
            _context.Blogs.Add(userBlog.Blog);
            _context.SaveChanges();
            userBlog.BlogId = userBlog.Blog.Id;
            _context.UserBlogs.Add(userBlog);
            _context.SaveChanges();
        }

        public IEnumerable<UserBlog>? GetAll()
        {
            return _context.UserBlogs?.Include(userBlog => userBlog.Blog).Include(userBlog => userBlog.AppUser).OrderByDescending(userBlog => userBlog.Blog.DatePublished);
        }

        public IEnumerable<UserBlog>? GetAllByAppUserID(string appUserID)
        {
            return _context.UserBlogs?.Where(userBlog => userBlog.AppUserId == appUserID).Include(userBlog => userBlog.Blog).Include(userBlog => userBlog.AppUser).OrderByDescending(userBlog => userBlog.Blog.DatePublished);
        }
    }
}
