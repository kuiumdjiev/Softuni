using System.Xml.Serialization;
using TeisterMask.Data.Models;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectExportModel
    {
        [XmlAttribute("TasksCount")] 
        public string TasksCount { get; set; }


        [XmlAttribute("ProjectName")]

        public string ProjectName { get; set; }

        [XmlAttribute("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskExportModel[] Tasks { get; set; }
    }
}