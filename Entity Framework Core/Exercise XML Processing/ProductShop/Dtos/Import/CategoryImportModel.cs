using System.Xml.Serialization;

namespace ProductShop.Dtos.Import
{
    [XmlType("Category")]
    public class CategoryImportModel
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
   // <Category>
  //  <name>Drugs</name>
   // </Category>
}