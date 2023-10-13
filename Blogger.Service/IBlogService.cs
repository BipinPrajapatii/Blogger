using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogger.Data.Entities;
using Blogger.Data.Migrations;
using Blogger.Shared;

namespace Blogger.Service
{
    public interface IBlogService
    {
        IEnumerable<BlogViewModel>? GetAll();
        void Update(BlogSaveModel model);
        bool DeleteById(int id);
        BlogSaveModel GetById(int id);

    }
}
