using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.ViewModels;

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
        public ActionResult<StudentViewModel> Get([FromRoute] long id)
        {
            var student = StudentRepository.GetIncludingCourses(id);

            if (student == null)
                return NotFound();

            var studentViewModel = Mapper.Map<Student, StudentViewModel>(student);

            return Ok(studentViewModel);
        }

        // localhost/student/
        [HttpGet()]
        public ActionResult<List<StudentViewModel>> GetAll()
        {
            var students = StudentRepository.GetAll();

            var studentViewModels = new List<StudentViewModel>();
            foreach (var student in students)
            {
                var studentViewModel = Mapper.Map<Student, StudentViewModel>(student);

                studentViewModels.Add(studentViewModel);
            }

            return Ok(studentViewModels);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentViewModel studentDetails)
        {
            var student = Mapper.Map<StudentViewModel, Student>(studentDetails);

            StudentRepository.Add(student);
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
