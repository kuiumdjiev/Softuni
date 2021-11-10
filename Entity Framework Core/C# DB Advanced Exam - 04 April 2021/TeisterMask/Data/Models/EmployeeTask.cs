using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class EmployeeTask
    {
        [Key]
        public int 	EmployeeId  { get; set; }

        public Employee Employee { get; set; }

        [Key]
        public int TaskId { get; set; }

        public Task Task { get; set; }
    }
}