using System.Numerics;
using System.Xml.Serialization;

namespace CarDealer.DTO
{
    [XmlType("car")]
    public class ExportCar
    { 
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("travelled-distance")]
        public BigInteger Travelleddistance { get; set; }
    }
}