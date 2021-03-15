using EpicUniversity.Models;
using EpicUniversity.Repository;
using Moq;

namespace EpicUniversity.Test.Mock
{
    public class MockCourseRepository
    {
        public Mock<ICourseRepository> mockCourseRepository;

        public ICourseRepository Object => mockCourseRepository.Object;

        public MockCourseRepository()
        {
            mockCourseRepository = new Mock<ICourseRepository>();

            mockCourseRepository.Setup(x => x.GetIncludingProfessorsStudents(It.Is<long>(s => s == 1)))
                .Returns(new Course { Id = 1 });
        }

        public void GetIncludingProfessorsStudents(int courseId, Course course)
        {
            mockCourseRepository.Setup(x => x.GetIncludingProfessorsStudents(It.Is<long>(s => s == courseId)))
                .Returns(course);
        }
    }
}
