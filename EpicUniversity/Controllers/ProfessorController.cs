using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicUniversity.Models;
using EpicUniversity.Repository;
using EpicUniversity.ViewModels;
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
        public ActionResult<ProfessorViewModel> Get([FromRoute] long id)
        {
            var proff = ProfessorRepository.GetProfessorWithCourseInfo(id);

            if (proff == null) return NotFound();
            var professorvm = Mapper.Map<Professor, ProfessorViewModel>(proff);

            return Ok(professorvm);
        }
        [HttpGet("Professor/{name}")]
        public ActionResult<IList<ProfessorViewModel>> Get([FromRoute] string name)
        {
            var professors = ProfessorRepository.GetProfessorWithCourseInfoByName(name);

            if (professors == null) return NotFound();
            IList<ProfessorViewModel> listOfProfessor = new List<ProfessorViewModel>();
            foreach (Professor p in professors)
            {
               var professorViewModel = new ProfessorViewModel
                {
                    FirstName=p.FirstName,
                    LastName=p.LastName,
                    Tenure=p.Tenure
                };
                listOfProfessor.Add(professorViewModel);

            }
            return Ok(listOfProfessor);


        }

        [HttpPost]
        public IActionResult Create([FromBody] ProfessorViewModel newProfessor)
        {
            var newProf = Mapper.Map<ProfessorViewModel, Professor>(newProfessor);
          
           ProfessorRepository.Add(newProf);
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
