using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication26.Models;

namespace WebApplication26.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
             "Data Source=.\\sqlexpress;Initial Catalog=BlogPosts ;Integrated Security=True";


        public IActionResult Index()
        {
            BlogDB db = new BlogDB(_connectionString);
            BlogPostViewModel vm = new BlogPostViewModel
            {
                Blogs = db.GetBlogPosts(),
            };
            return View(vm);
        }
        public IActionResult ViewBlog(int id)
        {
            BlogDB db = new BlogDB(_connectionString);
            SinglePostViewModel vm = new SinglePostViewModel
            {
                Blogs = db.GetPost(id),
                Commenter = Request.Cookies["name"]
               
            };
            return View(vm);
        }
        public IActionResult MostRecent()
        {
            BlogDB db = new BlogDB(_connectionString);
            IEnumerable<BlogPost> blogPosts = db.GetBlogPosts();
            SinglePostViewModel vm = new SinglePostViewModel
            {
                Blogs = blogPosts.FirstOrDefault(),
                Commenter = Request.Cookies["name"]
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddComment(Comments c)
        {
            BlogDB db = new BlogDB(_connectionString);
            db.AddComment(c);
            Response.Cookies.Append("name", c.CommenterName);
            return Redirect($"/Home/ViewBlog?id={c.BlogPostId}");
        }

        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitPost(BlogPost bp)
        {
            BlogDB db = new BlogDB(_connectionString);
            db.AddPost(bp);
            return Redirect($"/Home/Index");
        }



       

        
    }
}
