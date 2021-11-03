using System.Xml.Serialization;

namespace CarDealer.DTO
{
    [XmlType("car")]
    public class ExporCarWithParts
    {
        [XmlElement("parts")]
        public PartsCarExport[] Parts { get; set; }

        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelleDdistance { get; set; }

        
    }
}