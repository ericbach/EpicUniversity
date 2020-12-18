using System.Collections.Generic;

namespace EpicUniversity.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public int Credits { get; set; }

        // one-to-one relationship
        public CourseLab CourseLab { get; set; }

        // many-to-many relationship
        public IList<Student> Students { get; set; } = new List<Student>();

        // one-to-many relationship
        public Professor Professor { get; set; }

        public IList<Grade> Grades { get; set; }
    }

    public class CourseLab : Entity
    {
        public string Name { get; set; }

        public long CourseId { get; set; }
        public Course Course { get; set; }
    }
}
