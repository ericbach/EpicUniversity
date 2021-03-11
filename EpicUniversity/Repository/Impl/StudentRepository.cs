using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository.Impl
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context) : base(context)
        {
            _context = context;
        }

        public virtual Student GetIncludingCourses(long id)
        {
            return _context.Students
                .Include(s => s.Courses)
                .FirstOrDefault(s => s.Id == id);
        }

        //public ICollection<Student> GetAllStudentsWithGPA()
    }
}
