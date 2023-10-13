using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blogger.Data.Entities;
using Blogger.Repo;
using Blogger.Shared;

namespace Blogger.Service
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(IGenericRepository<Blog> blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public bool DeleteById(int id)
        {
            bool isDeleted = false;
            try
            {
                var blog = _blogRepository.Get(id);
                if (blog != null)
                {
                    _blogRepository.Delete(blog);
                    isDeleted = true;
                }
            }
            catch
            {
                isDeleted = false;
            }
            return isDeleted;

        }

        public IEnumerable<Blog> GetAll(FilterModel filter)
        {
            if (filter.PageNumber == 1)
            {
                return _blogRepository.GetAll().Where(blog => blog.Title.Contains(filter.Search)).Take(filter.PageSize);
            }
            else
            {
                return _blogRepository.GetAll().Where(blog => blog.Title.Contains(filter.Search)).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            }
        }

        public IEnumerable<BlogViewModel>? GetAll()
        {
            var blogs = _mapper.Map<IEnumerable<BlogViewModel>>(_blogRepository.GetAll());
            return blogs;
        }

        public BlogSaveModel GetById(int id)
        {
            var model = _mapper.Map<BlogSaveModel>(_blogRepository.Get(id));
            return model;
        }

        public void Update(BlogSaveModel model)
        {
            var blog = _mapper.Map<Blog>(model);
            _blogRepository.Update(blog);
        }
    }
}
