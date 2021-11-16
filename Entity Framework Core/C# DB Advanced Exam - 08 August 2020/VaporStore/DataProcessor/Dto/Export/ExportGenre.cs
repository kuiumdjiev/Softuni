using System.Xml.Serialization;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Export
{
   
    public class ExportGenre
    {
      
        public int Id { get; set; }

        public string Genre { get; set; }

       
        public ExportGame[]  Games { get; set; }
    }
}