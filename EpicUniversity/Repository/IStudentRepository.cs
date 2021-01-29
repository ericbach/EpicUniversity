using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicUniversity.Models;

namespace EpicUniversity.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student GetIncludingCourses(long id);
        //ICollection<Student> GetAllStudentsWithGPA();
    }
}
