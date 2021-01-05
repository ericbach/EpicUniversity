using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly UniversityContext _context;

        public CourseRepository(UniversityContext context) : base(context)
        {
            _context = context;
        }
    }
}
