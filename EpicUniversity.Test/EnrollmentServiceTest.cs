using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Models;
using EpicUniversity.Services;
using EpicUniversity.Test.Mock;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EpicUniversity.Test
{
    [TestClass]
    public class EnrollmentServiceTest
    {
        /// <summary>
        /// 1. Fast
        /// 2. Isolated
        /// 3. Repeatable
        /// 4. Predictable
        /// </summary>
        [TestMethod]
        public void ValidStudentEnrollment_Should_EnrollStudentInCourse()
        {
            // ARRANGE
            var courseId = 1;
            var studentId = 1;

            var mockCourseRepository = new MockCourseRepository();
            mockCourseRepository.GetIncludingProfessorsStudents(courseId, new Course
            {
                Students = new List<Student>(),
                Credits = 2
            });

            var mockStudent = new Student
            {
                Courses = new List<Course>
                {
                    new Course
                    {
                        Credits = 1
                    }
                }
            };
            var mockStudentRepository = new MockStudentRepository();
            mockStudentRepository.GetIncludingCourses(studentId, mockStudent);

            // ACT
            var enrollmentService = new EnrollmentService(mockCourseRepository.Object, mockStudentRepository.Object);
            var result = enrollmentService.Enroll(1, 1);

            // ASSERT
            result.Should().BeEmpty("Successfully enrolling in a course should not return validation errors");
        }

        [TestMethod]
        [DataRow(3, 6, true)]
        [DataRow(3, 7, true)]
        [DataRow(3, 8, false)]
        [DataRow(3, 9, false)]
        public void Student_ShouldOnlyBeAbleToEnroll_InCourseWithLessThanTenCredits(int courseCredits, int studentCredits, bool shouldAllowEnrollment)
        {
            // ARRANGE
            var courseId = 1;
            var studentId = 1;

            // Course to enroll in
            var mockCourseRepository = new MockCourseRepository();
            mockCourseRepository.GetIncludingProfessorsStudents(courseId, new Course
            {
                Students = new List<Student>(),
                Credits = courseCredits
            });

            var mockStudent = new Student
            {
                Courses = new List<Course>
                {
                    new Course
                    {
                        Credits = studentCredits
                    }
                }
            };
            var mockStudentRepository = new MockStudentRepository();
            mockStudentRepository.GetIncludingCourses(studentId, mockStudent);

            // ACT
            var enrollmentService = new EnrollmentService(mockCourseRepository.Object, mockStudentRepository.Object);
            var result = enrollmentService.Enroll(1, 1);

            // ASSERT
            if (shouldAllowEnrollment)
                result.Should().BeEmpty();
            else
                result.First().Should().BeEquivalentTo("Student is already enrolled in more than 10 credits of courses");
        }
    }
}
