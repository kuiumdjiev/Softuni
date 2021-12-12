using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [RegularExpression("[A-Z]{2}[1-9]{4}[A-Z]{2}")]
        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}