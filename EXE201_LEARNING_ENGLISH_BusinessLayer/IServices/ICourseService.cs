using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ICourseService
    {
        public ResponseResult<CourseReponse> GetCourse(int id);
        public ResponseResult<CourseReponse> UpdateCourse(UpdateCourseRequest request, int id);
        public ResponseResult<CourseReponse> DeleteCourse(int id);
        public ResponseResult<CourseReponse> CreateCourse(CreateCourseRequest request);
        public DynamicModelsResponse<CourseReponse> GetCourses(CourseFilter request, PagingRequest paging);
        // fix sau
        public IList<Course> GetCoursesByTeacherId(int id);
    }
}
