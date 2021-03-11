using System;
using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

            var mockCourse = new Course
            {
                Students = new List<Student>(),
                Credits = 2
            };
            var mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseRepository.Setup(x => x.GetIncludingProfessorsStudents(It.Is<long>(s => s == courseId))).Returns(mockCourse);

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
            var mockStudentRepository = new Mock<IStudentRepository>();
            mockStudentRepository.Setup(x => x.GetIncludingCourses(It.Is<long>(s => s == studentId))).Returns(mockStudent);
            mockStudentRepository.Setup(x => x.Update(It.IsAny<Student>()));
            mockStudentRepository.Setup(x => x.SaveChanges());

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
            var mockCourse = new Course
            {
                Students = new List<Student>(),
                Credits = courseCredits
            };
            var mockCourseRepository = new Mock<ICourseRepository>();
            mockCourseRepository.Setup(x => x.GetIncludingProfessorsStudents(It.Is<long>(s => s == courseId))).Returns(mockCourse);

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
            var mockStudentRepository = new Mock<IStudentRepository>();
            mockStudentRepository.Setup(x => x.GetIncludingCourses(It.Is<long>(s => s == studentId))).Returns(mockStudent);
            mockStudentRepository.Setup(x => x.Update(It.IsAny<Student>()));
            mockStudentRepository.Setup(x => x.SaveChanges());

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
