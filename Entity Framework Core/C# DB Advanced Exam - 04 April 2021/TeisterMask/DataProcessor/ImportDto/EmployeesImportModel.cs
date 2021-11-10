using System.ComponentModel.DataAnnotations;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class EmployeesImportModel
    {
        [MinLength(3)]
        [MaxLength(40)]
        [RegularExpression("^[A-Za-z0-9]{3,}$")]
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}[-]{1}[0-9]{3}[-]{1}[0-9]{4}")]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}