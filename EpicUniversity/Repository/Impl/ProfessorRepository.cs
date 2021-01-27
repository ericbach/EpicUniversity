using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace EpicUniversity.Repository
{
    public class ProfessorRepository:Repository<Professor>,IProfessorRepository
    {
        private readonly UniversityContext _context;
        public ProfessorRepository(UniversityContext context) : base(context)
        {
            _context = context;
        }

        public Professor GetProfessorWithCourseInfo(long id)
        {
            
               return  _context.Professors.Include(c => c.Courses)
                                          .FirstOrDefault(c => c.Id == id);
            
        }
    }
}