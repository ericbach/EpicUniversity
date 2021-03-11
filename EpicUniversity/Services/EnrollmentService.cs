using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Repository;

namespace EpicUniversity.Services
{
    public interface IEnrollmentService
    {
        ICollection<string> Enroll(long studentId, long courseId);
    }

    public class EnrollmentService: IEnrollmentService
    {
        public ICourseRepository CourseRepository { get; set; }
        public IStudentRepository StudentRepository { get; set; }

        public EnrollmentService(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            CourseRepository = courseRepository;
            StudentRepository = studentRepository;
        }

        public ICollection<string> Enroll(long studentId, long courseId)
        {
            var validationErrors = new List<string>();

            var student = StudentRepository.GetIncludingCourses(studentId);
            var course = CourseRepository.GetIncludingProfessorsStudents(courseId);

            if (course.Students.Any(s => s.Id == studentId))
                validationErrors.Add("Student is already enrolled in course");

            if (course.Students.Count > 2)
                validationErrors.Add("Course is full");

            if (student.Courses.Sum(c => c.Credits) + course.Credits > 10)
                validationErrors.Add("Student is already enrolled in more than 10 credits of courses");

            student.Courses.Add(course);
            StudentRepository.Update(student);
            StudentRepository.SaveChanges();

            return validationErrors;
        }
    }
}
