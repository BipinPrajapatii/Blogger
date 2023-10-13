using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Shared
{
    public class PagingInfoModel
    {
        public int TotalItems { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 0;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
