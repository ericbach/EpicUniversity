using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using Newtonsoft.Json;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class ProfessorController : Controller
    {
        public IProfessorRepository ProfessorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            ProfessorRepository = professorRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<Professor> Get([FromRoute] long id)
        {
            var proff = ProfessorRepository.GetProfessorWithCourseInfo(id);

            if (proff == null) return NotFound();

            return Ok(JsonConvert.SerializeObject(proff, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Professor newProfessor)
        {
            //gives error
            var proffs = ProfessorRepository.GetAll();


            if (proffs == null || !proffs.Contains(newProfessor))
            {
                ProfessorRepository.Add(newProfessor);
                ProfessorRepository.SaveChanges();
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Professor professor)
        {
            var isExistingProfessor = ProfessorRepository.GetProfessorWithCourseInfo(professor.Id);

            if (isExistingProfessor == null) return BadRequest("No such professor exists");

            //update
            isExistingProfessor.Id = professor.Id;
            isExistingProfessor.FirstName = professor.FirstName;
            isExistingProfessor.LastName = professor.LastName;
            isExistingProfessor.ParkingSpot = professor.ParkingSpot;
            isExistingProfessor.Tenure = professor.Tenure;
            isExistingProfessor.Courses = professor.Courses;
            isExistingProfessor.Birthdate = professor.Birthdate;

            ProfessorRepository.Update(isExistingProfessor);
            ProfessorRepository.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(long id)
        {
            var isExistingProfessor = ProfessorRepository.Get(id);
            if (isExistingProfessor == null) return BadRequest("No such professor exists");

            //delete
            ProfessorRepository.Remove(isExistingProfessor.Id);
            ProfessorRepository.SaveChanges();

            return Ok();
        }
    }
}
