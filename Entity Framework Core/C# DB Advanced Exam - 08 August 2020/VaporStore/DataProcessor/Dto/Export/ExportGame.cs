using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    public class ExportGame
    {
       
        public int Id { get; set; }

      
        public string Title { get; set; }

      
        public string Developer { get; set; }

        
        public string Tags { get; set; }

       
        public int Players { get; set; }
    }
}