﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookShop.Data.Models.Enums;

namespace BookShop.Data.Models
{
    public class Book
    {
        public Book()
        {
            AuthorsBooks = new HashSet<AuthorBook>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(50,5000)]
        public int Pages { get; set; }

        [Required]
        public DateTime PublishedOn  { get; set; }

        public ICollection<AuthorBook> AuthorsBooks { get; set; }

    }
}