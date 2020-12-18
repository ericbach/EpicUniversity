using System;
using System.Collections.Generic;

namespace EpicUniversity.Models
{
    public class Personnel : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
    
    public class Student : Personnel
    {
        public decimal Gpa { get; set; }
        public IList<Course> Courses { get; set; } = new List<Course>();

        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }

    public class Professor : Personnel
    {
        public int Tenure { get; set; }
        public int ParkingSpot { get; set; }
        public IList<Course> Courses { get; set; } = new List<Course>();
    }
}