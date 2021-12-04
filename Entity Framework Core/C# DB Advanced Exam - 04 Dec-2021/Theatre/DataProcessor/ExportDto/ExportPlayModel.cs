using System.Xml;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ExportDto
{
    [XmlType("Play")]
    public class ExportPlayModel
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Duration")]

        public string Duration { get; set; }

        [XmlAttribute("Rating")]

        public string Rating { get; set; }

        [XmlAttribute("Genre")]

        public string Genre { get; set; }

        [XmlArray("Actors")]
        public ExportActorsMOdel[] Actors { get; set; }

    }
}