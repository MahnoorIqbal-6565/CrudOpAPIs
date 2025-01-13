﻿namespace EmployeeAdminPortal.Models.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public required string Name { get; set; }
     
        public required string Email { get; set; }

        public required string PhoneNumber { get; set; } 

        public string? Address {  get; set; }

        public string? City { get; set; }
    }
}
