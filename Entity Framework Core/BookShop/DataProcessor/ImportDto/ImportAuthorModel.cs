using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookShop.Data.Models;
using BookShop.Data.Models.Enums;

namespace BookShop.DataProcessor.ImportDto
{
    public class ImportAuthorModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [RegularExpression("[0-9]{3}[-]{1}[0-9]{3}[-]{1}[0-9]{4}")]
        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        
        public ImportAuthorBookModel[] Books { get; set; }

    }
}