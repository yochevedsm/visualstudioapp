using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication26.Models
{
    public class BlogPostViewModel
    {
        public List<BlogPost> Blogs { get; set; }
    }




    public class SinglePostViewModel
    {
        public BlogPost Blogs { get; set; }
        public string Commenter { get; set; }
    }
}

