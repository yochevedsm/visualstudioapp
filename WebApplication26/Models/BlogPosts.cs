using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication26.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateSubmitted { get; set; } 
        public List<Comments> Comments { get; set; }
    }







    public class Comments
    {
        public int Id { get; set; }
        public string CommenterName { get; set; }
        public string Text { get; set; }
        public DateTime DatePosted { get; set; }
        public int BlogPostId { get; set; }
    }

}
