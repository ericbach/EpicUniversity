using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicUniversity.Models;
using EpicUniversity.Repository;
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
        public ActionResult<Course> Get([FromRoute] long id)
        {
            var course = CourseRepository.GetIncludingProfessorsStudents(id);

            if (course == null)
                return NotFound();

            return Ok(JsonConvert.SerializeObject(course, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        // localhost/course/
        [HttpGet()]
        public ActionResult<List<Course>> GetAll()
        {
            return Ok(CourseRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Course courseDetails)
        {
            CourseRepository.Add(courseDetails);
            CourseRepository.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Course courseDetails)
        {
            var course = CourseRepository.Get(courseDetails.Id);
            if (course == null)
                return BadRequest("Course does not exist");

            course.Name = courseDetails.Name;
            course.Credits = courseDetails.Credits;

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
