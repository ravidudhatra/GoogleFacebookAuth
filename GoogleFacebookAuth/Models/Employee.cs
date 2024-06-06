using System.ComponentModel.DataAnnotations;

namespace GoogleFacebookAuth.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public long Salary { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }
    }
}
