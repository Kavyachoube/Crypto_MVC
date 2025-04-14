using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Crypto_MVC.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CryptoVerseDB;Trusted_Connection=True;";

    public IActionResult Index()
    {
        var users = new List<RegisterModel>();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Register", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new RegisterModel
                {
                    Username = reader["Username"].ToString(),
                    Role = reader["Role"].ToString(),
                    Email = reader["Email"].ToString()
                });
            }
        }

        ViewBag.TotalUsers = users.Count(u => u.Role == "User");
        ViewBag.TotalAdmins = users.Count(u => u.Role == "Admin");

        return View(users);
    }

    [HttpPost]
    public IActionResult Delete(string username)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Register WHERE Username = @Username AND Role != 'Admin'", con);
            cmd.Parameters.AddWithValue("@Username", username);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Index");
    }
}
