using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository.Impl
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context) : base(context)
        {
            _context = context;
        }

        public virtual Course GetIncludingProfessorsStudents(long id)
        {
            // Eager load Professors and Students
            return _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Course> GetAllCoursesWithCredit(int credits)
        {
            return Find(c => c.Credits == credits);
        }

        public ICollection<Course> GetCoursesWithMoreThan100Students()
        {
            return _context.Courses.Where(c => c.Students.Count() > 100).ToList();
        }
    }
}
