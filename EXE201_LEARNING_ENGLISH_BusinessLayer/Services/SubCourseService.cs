using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class SubCourseService : CourseService, ISubCourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;

        public SubCourseService(IGenericRepository<Course> repository, IMapper mapper) : base(repository, mapper)
        {
            _courseRepository = repository;
        }

        public ICollection<string> GetListEmailOfStudentsInCourse(int courseId)
        {
            ICollection<string> lstEmail = new List<string>();
            try
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                lstEmail = _courseRepository.Find(x => x.CourseId == courseId)
                                .StudentCourses.Select(a => a.Student.Email).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
            catch(Exception ex)
            {
                return lstEmail;
            }
            finally
            {
                lock (this) ;
            }
            return lstEmail;
        }
    }
}
