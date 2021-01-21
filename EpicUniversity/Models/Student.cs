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
        public virtual IList<Course> Courses { get; set; } = new List<Course>();
        public virtual IList<Grade> Grades { get; set; } = new List<Grade>();

        public Student()
        {
        }

        public Student(string firstName, string lastName, DateTime birthdate)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
        }
    }
}