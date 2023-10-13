using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data.Entities;

namespace Blogger.Repo
{
    public interface IUserBlogRepository
    {
        IEnumerable<UserBlog>? GetAll();
        IEnumerable<UserBlog>? GetAllByAppUserID(string appUserID);
        void Create(UserBlog userBlog);

    }
}
