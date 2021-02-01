using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.ViewModels;

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

            var courseVm = Mapper.Map<Course, CourseViewModel>(course);
            return Ok(courseVm);

            //var courseViewModel = new CourseViewModel
            //{
            //    Name = course.Name,
            //    Credits = course.Credits,
            //};

            //return Ok(courseViewModel);
        }
        
        // localhost/course/
        [HttpGet()]
        public ActionResult<List<CourseViewModel>> GetAll()
        {
            var courses = CourseRepository.GetAll();

            //var courseVM = Mapper.Map<ICollection<Course>, ICollection<CourseViewModel>>(courses);
            var courseViewModels = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                var courseViewModel = Mapper.Map<Course, CourseViewModel>(course);
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
                var courseViewModel = Mapper.Map<Course, CourseViewModel>(course);
                courseViewModels.Add(courseViewModel);
            }

            return Ok(courseViewModels);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CourseViewModel courseViewModel)
        {
            var course = Mapper.Map<CourseViewModel, Course>(courseViewModel);

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
