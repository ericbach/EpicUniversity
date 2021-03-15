using EpicUniversity.Models;
using EpicUniversity.Repository;
using Moq;

namespace EpicUniversity.Test.Mock
{
    public class MockStudentRepository
    {
        public Mock<IStudentRepository> mockStudentRepository;

        public IStudentRepository Object => mockStudentRepository.Object;

        public MockStudentRepository()
        {
            mockStudentRepository = new Mock<IStudentRepository>();
            
            mockStudentRepository.Setup(x => x.GetIncludingCourses(It.Is<long>(s => s == 1))).Returns(new Student());
            mockStudentRepository.Setup(x => x.Update(It.IsAny<Student>()));
            mockStudentRepository.Setup(x => x.SaveChanges());
        }

        public void GetIncludingCourses(int studentId, Student student)
        {
            mockStudentRepository.Setup(x => x.GetIncludingCourses(It.Is<long>(s => s == studentId))).Returns(student);
        }
    }
}
