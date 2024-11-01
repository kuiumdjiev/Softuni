﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTicketModel
    {
        [Required]
        [Range(1.00, 100.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public sbyte RowNumber { get; set; }

        [Required]
        public int PlayId { get; set; }

    }
}