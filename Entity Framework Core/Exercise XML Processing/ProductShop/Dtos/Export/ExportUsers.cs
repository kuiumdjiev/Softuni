using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("User")]
    public class ExportUsers
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("soldProducts")]
        public ExportUsersSoldProducts[] SoldProducts { get; set; }
    }
}