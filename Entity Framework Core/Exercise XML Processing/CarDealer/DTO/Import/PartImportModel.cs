using System.Xml.Serialization;

namespace CarDealer.DTO.Import
{
    [XmlType("parts")]
    public class PartImportModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
    //    < parts >
    //    < partId id = "38" />
}