using System.Collections.Generic;
using EpicUniversity.Models;

namespace EpicUniversity.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetIncludingProfessorsStudents(long id);
        ICollection<Course> GetAllCoursesWithCredit(int credits);
        ICollection<Course> GetCoursesWithMoreThan100Students();
    }
}