using System;
using System.Collections.Generic;

namespace EpicUniversity.Models
{
    public class Professor : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public int Tenure { get; set; }
        public int ParkingSpot { get; set; }
        public virtual IList<Course> Courses { get; set; } = new List<Course>();
    }
}