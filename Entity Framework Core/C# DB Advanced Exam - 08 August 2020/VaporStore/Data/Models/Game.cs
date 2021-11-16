using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class Game
    {
        public Game()
        {
            Purchases = new HashSet<Purchase>();
            GameTags = new HashSet<GameTag>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate  { get; set; }

        [Required]
        public int DeveloperId { get; set; }

        [Required]
        public Developer Developer { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public ICollection<Purchase> 	Purchases  { get; set; }

        [Required]
        public ICollection<GameTag> 	GameTags  { get; set; }
    }
}