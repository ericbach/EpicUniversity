using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.ViewModels;
using Newtonsoft.Json;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")] // [Route("course")]
    public class CourseController : Controller
    {
        public ICourseRepository CourseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            CourseRepository = courseRepository;
        }

        // localhost/course/1
        [HttpGet("{id}")]
        public ActionResult<CourseViewModel> Get([FromRoute] long id)
        {
            var course = CourseRepository.GetIncludingProfessorsStudents(id);

            if (course == null)
                return NotFound();

            var courseViewModel = new CourseViewModel
            {
                Name = course.Name,
                Credits = course.Credits
            };

            return Ok(courseViewModel);
        }
        
        // localhost/course/
        [HttpGet()]
        public ActionResult<List<CourseViewModel>> GetAll()
        {
            var courses = CourseRepository.GetAll();

            var courseViewModels = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel
                {
                    Name = course.Name,
                    Credits = course.Credits
                };

                courseViewModels.Add(courseViewModel);
            }
            
            return Ok(courseViewModels);
        }

        // localhost/course/credits/1
        [HttpGet("credits/{credits}")]
        public ActionResult<CourseViewModel> GetCourseWithCredits([FromRoute] int credits)
        {
            var courses = CourseRepository.GetAllCoursesWithCredit(credits);

            if (courses == null)
                return NotFound();

            var courseViewModels = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel
                {
                    Name = course.Name,
                    Credits = course.Credits
                };
                courseViewModels.Add(courseViewModel);
            }

            return Ok(courseViewModels);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CourseViewModel courseViewModel)
        {
            var course = new Course
            {
                Name = courseViewModel.Name,
                Credits = courseViewModel.Credits
            };

            CourseRepository.Add(course);
            CourseRepository.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CourseUpdateViewModel updatedCourse)
        {
            var course = CourseRepository.Get(updatedCourse.Id);
            if (course == null)
                return BadRequest("Course does not exist");

            course.Name = updatedCourse.Course.Name;
            course.Credits = updatedCourse.Course.Credits;

            CourseRepository.Update(course);
            CourseRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var course = CourseRepository.Get(id);
            if (course == null)
                return BadRequest("Course does not exist");

            CourseRepository.Remove(course);
            CourseRepository.SaveChanges();

            return Ok();
        }
    }
}
