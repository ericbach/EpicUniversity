using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using Newtonsoft.Json;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")] // [Route("student")]
    public class StudentController : Controller
    {
        public IStudentRepository StudentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            StudentRepository = studentRepository;
        }

        // localhost/student/1
        [HttpGet("{id}")]
        public ActionResult<Student> Get([FromRoute] long id)
        {
            var student = StudentRepository.GetIncludingCourses(id);

            if (student == null)
                return NotFound();

            return Ok(JsonConvert.SerializeObject(student, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        // localhost/student/
        [HttpGet()]
        public ActionResult<List<Student>> GetAll()
        {
            return Ok(StudentRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student studentDetails)
        {
            StudentRepository.Add(studentDetails);
            StudentRepository.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Student studentDetails)
        {
            var student = StudentRepository.Get(studentDetails.Id);
            if (student == null)
                return BadRequest("Student does not exist");

            student.FirstName = studentDetails.FirstName;
            student.LastName = studentDetails.LastName;
            student.Birthdate = studentDetails.Birthdate;

            StudentRepository.Update(student);
            StudentRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var student = StudentRepository.Get(id);
            if (student == null)
                return BadRequest("Student does not exist");

            StudentRepository.Remove(student);
            StudentRepository.SaveChanges();

            return Ok();
        }
    }
}
