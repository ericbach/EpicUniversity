using System.Collections.Generic;

namespace EpicUniversity.Models
{
    public class Course : Entity
    {
        public string Name { get; set; }
        public int Credits { get; set; }

        // one-to-one relationship
        public virtual CourseLab CourseLab { get; set; }

        // many-to-many relationship
        public virtual IList<Student> Students { get; set; } = new List<Student>();

        // one-to-many relationship
        public virtual Professor Professor { get; set; }

        public virtual IList<Grade> Grades { get; set; }
    }

    public class CourseLab : Entity
    {
        public string Name { get; set; }

        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
