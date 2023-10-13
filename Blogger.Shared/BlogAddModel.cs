using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Shared
{
    public class BlogSaveModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; }
        public string AppUserID { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
