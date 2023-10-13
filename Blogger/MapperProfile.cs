using AutoMapper;
using Blogger.Data.Entities;
using Blogger.Shared;

namespace Blogger
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserBlog, BlogViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BlogId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Blog.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Blog.Content))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.DatePublished, opt => opt.MapFrom(src => src.Blog.DatePublished));

            CreateMap<Blog, BlogSaveModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.DatePublished, opt => opt.MapFrom(src => src.DatePublished))
                .ReverseMap();

            //CreateMap<BlogEditModel, Blog>()
            //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            //   .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            //   .ForMember(dest => dest.DatePublished, opt => opt.MapFrom(src => src.DatePublished));

        }
    }
}
