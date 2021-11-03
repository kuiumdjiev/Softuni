using System.Xml.Serialization;

namespace CarDealer.DTO
{
    [XmlType("car")]

    public class ExportBMW
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long Travelleddistance { get; set; }

    }
}