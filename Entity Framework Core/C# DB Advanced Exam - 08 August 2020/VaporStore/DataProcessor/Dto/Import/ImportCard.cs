using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportCard
    {

        [RegularExpression("[0-9]{4}[ ]{1}[0-9]{4}[ ]{1}[0-9]{4}[ ]{1}[0-9]{4}")]
        [Required]
        public string Number { get; set; }
        
        [RegularExpression("[0-9]{3}")]
        [Required]
        public string CVC { get; set; }
        
        [Required]
        public string Type { get; set; }
    }
}