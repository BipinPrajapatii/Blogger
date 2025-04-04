﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Data.Entities
{
    public class UserBlog : BaseEntity
    {
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
