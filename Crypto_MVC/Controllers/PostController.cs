using Crypto_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Crypto_MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IConfiguration _configuration;

        public PostController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // View All Posts
        public IActionResult Index()
        {
            List<PostModel> posts = new List<PostModel>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Posts", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    posts.Add(new PostModel
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Content = reader["Content"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        UpdatedAt = reader["UpdatedAt"] as DateTime?
                    });
                }
            }
            return View(posts);
        }

        // Create Post
        [HttpPost]
        public IActionResult Create(PostModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Posts (Title, Content, CreatedAt) VALUES (@Title, @Content, @CreatedAt)", con);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Content", model.Content);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        // Edit Post (GET)
        public IActionResult Edit(int id)
        {
            PostModel post = new PostModel();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Posts WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    post.Id = (int)reader["Id"];
                    post.Title = reader["Title"].ToString();
                    post.Content = reader["Content"].ToString();
                }
            }
            return View(post);
        }

        // Edit Post (POST)
        [HttpPost]
        public IActionResult Edit(PostModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Posts SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Content", model.Content);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Delete Post
        public IActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("con")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Posts WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
