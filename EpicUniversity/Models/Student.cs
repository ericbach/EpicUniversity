using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicUniversity.Models
{
    public class Student : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        [NotMapped]
        public decimal Gpa { get; set; }
        public IList<Course> Courses { get; set; } = new List<Course>();
        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }
}