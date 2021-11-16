using System;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class GamesImportModel
    {

        [Required] 
        public string Name { get; set; }

        [Required]
        [Range(0,Double.MaxValue)]
        public decimal Price { get; set; }
        
        [Required]
        public string ReleaseDate { get; set; }
        
        [Required]
        public string Developer { get; set; }
        
        [Required]
        public string Genre { get; set; }
        
        [Required]
        public string[] Tags { get; set; }
    }
}