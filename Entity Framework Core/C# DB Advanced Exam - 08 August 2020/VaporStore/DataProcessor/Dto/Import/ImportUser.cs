using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUser
    {
        [Required]
        [RegularExpression("[A-Z]{1}[a-z]{1,}[ ]{1}[A-Z]{1}[a-z]{1,}")]
        public string FullName { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [Range(3,103)]
        public int Age { get; set; }

        public ImportCard[] Cards { get; set; }

    }
}