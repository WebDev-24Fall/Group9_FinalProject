using System.ComponentModel.DataAnnotations;

namespace Group9_FinalProject.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // Example: "Admin"
    }
}
