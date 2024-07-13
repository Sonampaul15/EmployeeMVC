using System.ComponentModel.DataAnnotations;

namespace EmployeeApiMVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name here")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Salary here")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "Enter City here")]
        public string city { get; set; }
    }
}
