using System.ComponentModel.DataAnnotations;

namespace MyFirstWebAPIProject.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50)]

        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Grade cannot be longer than 2 characters.")]
        public string Grade { get; set; }
    }
}
