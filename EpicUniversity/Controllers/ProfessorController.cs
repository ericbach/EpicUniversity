using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.ViewModels;

namespace EpicUniversity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : Controller
    {
        public IProfessorRepository ProfessorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            ProfessorRepository = professorRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<ProfessorViewModel> Get([FromRoute] long id)
        {
            var prof = ProfessorRepository.GetProfessorWithCourseInfo(id);

            if (prof == null) return NotFound();

            var professorViewModel = Mapper.Map<Professor, ProfessorViewModel>(prof);

            return Ok(professorViewModel);
        }

        [HttpGet("name={name}")]
        public ActionResult<IList<ProfessorViewModel>> Get([FromRoute] string name)
        {
            var professors = ProfessorRepository.GetProfessorWithCourseInfoByName(name);

            if (professors == null) return NotFound();

            var listOfProfessor = professors.Select(p => Mapper.Map<Professor, ProfessorViewModel>(p)).ToList();

            return Ok(listOfProfessor);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProfessorViewModel newProfessor)
        {
            var professor = Mapper.Map<ProfessorViewModel, Professor>(newProfessor);

            ProfessorRepository.Add(professor);
            ProfessorRepository.SaveChanges();
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
            ProfessorRepository.Remove(isExistingProfessor);
            ProfessorRepository.SaveChanges();

            return Ok();
        }
    }
}
