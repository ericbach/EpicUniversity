using EpicUniversity.Models;

namespace EpicUniversity.Repository
{
    public interface IProfessorRepository:IRepository<Professor>
    {
        Professor GetProfessorWithCourseInfo(long id);
    }
}