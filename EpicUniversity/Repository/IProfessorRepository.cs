using EpicUniversity.Models;
using System.Collections.Generic;

namespace EpicUniversity.Repository
{
    public interface IProfessorRepository:IRepository<Professor>
    {
        Professor GetProfessorWithCourseInfo(long id);
        IEnumerable<Professor> GetProfessorWithCourseInfoByName(string name);
    }
}