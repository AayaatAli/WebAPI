using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPIProject.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [PasswordPropertyText]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
