using AutoMapper;
using Blogger.Data.Entities;
using Blogger.Shared;

namespace Blogger.Service
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap(typeof(Blog), typeof(BlogViewModel)).ReverseMap();
        }
    }
}
