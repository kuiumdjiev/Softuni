﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            EmployeesTasks = new HashSet<EmployeeTask>();
        }
        [Key]
        public int  Id { get; set; }

        [Required] 
        [MinLength(3)]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks  { get; set; }
    }
}