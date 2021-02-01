﻿using System.Collections.Generic;
using System.Linq;
using EpicUniversity.Data;
using EpicUniversity.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EpicUniversity.Repository
{
    public class ProfessorRepository:Repository<Professor>,IProfessorRepository
    {
        private readonly UniversityContext _context;
        public ProfessorRepository(UniversityContext context) : base(context)
        {
            _context = context;
        }

        public Professor GetProfessorWithCourseInfo(long id)
        {
            
               return  _context.Professors.Include(c => c.Courses)
                                          .FirstOrDefault(c => c.Id == id);
            
        }

        public IEnumerable<Professor> GetProfessorWithCourseInfoByName(string name)
        {
            return Find(c => c.FirstName.ToLower() == name.ToLower() || c.LastName.ToLower() == name.ToLower());
            //return _context.Professors.Include(c => c.Courses)
            //    .ToList().Where(c => c.FirstName.ToLower() == name.ToLower() || c.LastName.ToLower() == name.ToLower());
        }
    }
}