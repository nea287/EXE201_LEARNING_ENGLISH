using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _repository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository<Course> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<CourseReponse> CreateCourse(CreateCourseRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<CourseReponse> DeleteCourse(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<CourseReponse> GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public DynamicModelResponse.DynamicModelsResponse<CourseReponse> GetCourses(CourseFilter request, PagingRequest paging)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<CourseReponse> UpdateCourse(UpdateCourseRequest request, int id)
        {
            throw new NotImplementedException();
        }
    }
}
