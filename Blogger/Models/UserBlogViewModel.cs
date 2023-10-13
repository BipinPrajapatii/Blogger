using Blogger.Shared;

namespace Blogger.Models
{
    public class UserBlogViewModel
    {
        public IEnumerable<Shared.BlogViewModel>? Blogs { get; set; }
        public PagingInfoModel? PagingInfo { get; set; }
        public string SearchFor { get; set; } = string.Empty;
    }
}
