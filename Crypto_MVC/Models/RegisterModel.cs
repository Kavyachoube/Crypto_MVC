using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Security.Cryptography;

namespace Crypto_MVC.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter Fullname")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Please enter Mobile No")]
        public string? MobileNumber { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter Country")]
        public string? Country { get; set; }

        public string? Role { get; set; }

        private string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CryptoVerseDB;Trusted_Connection=True;";

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Insert a record into a database table
        public bool Insert(RegisterModel re)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Register (Username, Password, Fullname, Address, MobileNumber, Email, Country, Role) " +
                        "VALUES (@Username, @Password, @Fullname, @Address, @MobileNumber, @Email, @Country, @Role)", con);

                    cmd.Parameters.AddWithValue("@Username", re.Username);
                    cmd.Parameters.AddWithValue("@Password", re.Password);
                    cmd.Parameters.AddWithValue("@Fullname", re.Fullname);
                    cmd.Parameters.AddWithValue("@Address", re.Address);
                    cmd.Parameters.AddWithValue("@MobileNumber", re.MobileNumber);
                    cmd.Parameters.AddWithValue("@Email", re.Email);
                    cmd.Parameters.AddWithValue("@Country", re.Country);
                    cmd.Parameters.AddWithValue("@Role", re.Role ?? "User");

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
