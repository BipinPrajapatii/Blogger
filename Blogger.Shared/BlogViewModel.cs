using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Shared
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
