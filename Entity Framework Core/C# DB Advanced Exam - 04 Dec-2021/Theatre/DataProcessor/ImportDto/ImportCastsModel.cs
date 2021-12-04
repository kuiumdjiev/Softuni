using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class ImportCastsModel
    {

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [XmlElement("FullName")]
        public string FullName { get; set; }

        [Required]
        [XmlElement("IsMainCharacter")]
        public bool IsMainCharacter { get; set; }

        [Required]
        [RegularExpression("[+44]{3}[-]{1}[0-9]{2}[-]{1}[0-9]{3}[-]{1}[0-9]{4}")]
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [XmlElement("PlayId")]
        public int PlayId { get; set; }
    }
}