using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Data
{
    public class BloggerDbContext : IdentityDbContext<AppUser>
    {
        public BloggerDbContext(DbContextOptions<BloggerDbContext> options) : base(options) { }
        public DbSet<AppUser>? AppUsers { get; set; }
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<UserBlog>? UserBlogs { get; set; }
    }
}
