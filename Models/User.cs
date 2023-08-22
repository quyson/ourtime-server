using System.ComponentModel.DataAnnotations;

namespace ourTime_server.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";

        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters")]
        public string Password { get; set; } = "";
    }
}
