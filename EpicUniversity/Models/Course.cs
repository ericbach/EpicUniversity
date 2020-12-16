using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace EpicUniversity.Models
{
    // POCO = Plain Old CLR Objects
    public class Course
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<Student> Students { get; set; } = new List<Student>();
        public Professor Professor { get; set; }
    }

    public class Personnel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class Student : Personnel
    {
        public IList<Course> Courses { get; set; } = new List<Course>();
    }

    public class Professor : Personnel
    {
        public int Tenure { get; set; }
        public int ParkingSpot { get; set; }
        public IList<Course> Courses { get; set; } = new List<Course>();
    }
}
