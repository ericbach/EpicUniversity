using System.Collections.Generic;
using AutoMapper;
using EpicUniversity.Models;

namespace EpicUniversity.ViewModels
{
    public static class Mapper
    {
        private static readonly IMapper AutoMapper;
        
        static Mapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>().ReverseMap();
                //cfg.CreateMap<List<Course>, List<CourseViewModel>>().ReverseMap();

                cfg.CreateMap<Student, StudentViewModel>().ReverseMap();
                cfg.CreateMap<Professor, ProfessorViewModel>()
                    .ForMember(d => d.NumberOfCoursesOfferedByProfessor,
                        o => o.MapFrom(s => s.Courses.Count));

                cfg.CreateMap<ProfessorViewModel, Professor>();
            });

            AutoMapper = configuration.CreateMapper();
        }

        public static TDest Map<TSource, TDest>(TSource source) where TSource : class where TDest : class
        {
            return AutoMapper.Map<TSource, TDest>(source);
        }
    }
}
