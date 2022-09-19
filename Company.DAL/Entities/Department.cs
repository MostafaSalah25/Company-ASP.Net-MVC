using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.DAL.Entities
{
    public class Department 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required")]

        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(100 ,MinimumLength =5 , ErrorMessage ="Name Length must be between 5 and 100 chars")]
        public string Name { get; set; }
     
        public DateTime DateOfCreation { get; set; }

        // Relationship between Employee and Department

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
