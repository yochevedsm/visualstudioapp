using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication26.Models
{
    public class BlogDB
    {
        private readonly string _connectionString;
        public BlogDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BlogPost> GetBlogPosts()
        {
            List<BlogPost> Blogs = new List<BlogPost>();
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM BlogPosts Order By Id desc";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Blogs.Add(new BlogPost
                {
                    Id = (int)reader["Id"],                  
                    Title = (string)reader["Title"],
                    Text = (string)reader["Text"],
                    DateSubmitted = (DateTime)reader["DateSubmitted"],
               

                });
            }
            connection.Dispose();
            return Blogs;
        }

        public void AddPost(BlogPost Blogs)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO BlogPosts VALUES ( @Title, @Text, @DateSubmitted )";
            cmd.Parameters.AddWithValue("@Title", Blogs.Title);
            cmd.Parameters.AddWithValue("@Text", Blogs.Text);
            cmd.Parameters.AddWithValue("@DateSubmitted", DateTime.Now);
            connection.Open();
            cmd.ExecuteNonQuery();
        
        }
        public void AddComment(Comments c)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Comments " +
                "VALUES( @CommenterName, @Text, @DatePosted, @BlogPostId)";
            
            cmd.Parameters.AddWithValue("@CommenterName", c.CommenterName);
            cmd.Parameters.AddWithValue("@Text", c.Text);
            cmd.Parameters.AddWithValue("@DatePosted", DateTime.Now);
            cmd.Parameters.AddWithValue("@BlogPostId", c.BlogPostId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public BlogPost GetPost(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From BlogPosts WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<BlogPost> blogPosts = new List<BlogPost>();
            if (!reader.Read())
            {
                return null;
            }
            BlogPost bp = new BlogPost
            {
                Id = (int)reader["Id"],
                Title = (string)reader["Title"],
                Text = (string)reader["Text"],                
                DateSubmitted = (DateTime)reader["DateSubmitted"],
                Comments = GetCommentsForPost(id)
            };

            return bp;
        }

        public List<Comments> GetCommentsForPost(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * From Comments WHERE BlogPostId = @id ";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Comments> comments = new List<Comments>();
            while (reader.Read())
            {
                comments.Add(new Comments
                {
                    Id = (int)reader["Id"],
                    CommenterName = (string)reader["CommenterName"],
                    Text = (string)reader["Text"],                   
                    DatePosted = (DateTime)reader["DatePosted"],
                    BlogPostId = (int)reader["BlogPostId"],
                });
            }
            return comments;
        }

    }
}
