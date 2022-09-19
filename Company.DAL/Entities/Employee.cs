using System;
using System.ComponentModel.DataAnnotations;

namespace Company.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; } 
        public DateTime CreationDate { get; set; } = DateTime.Now;
        // Relationship between Employee and Department
        public int DepartmentId { get; set; } 
        public  Department Department { get; set; } // Navigational Property work Eager Loading
        // deal with files as img 
        public string ImageName { get; set; } 


    }
}
