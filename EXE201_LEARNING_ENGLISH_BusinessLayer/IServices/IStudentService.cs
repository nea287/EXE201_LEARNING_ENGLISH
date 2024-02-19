using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.StudentCourse;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IStudentService
    {
        public ResponseResult<StudentReponse> GetStudent(int id);
        public ResponseResult<StudentReponse> GetStudent(string email);
        public ResponseResult<StudentReponse> UpdateStudent(UpdateStudentRequest request, int id);
        public ResponseResult<StudentReponse> DeleteStudent(int id);
        public ResponseResult<StudentReponse> CreateStudent(CreateStudentRequest createStudentRequest);
        public DynamicModelsResponse<StudentReponse> GetStudents(StudentFilter request, PagingRequest paging);
        public ResponseResult<StudentCourseReponse> CreateStudentCourse(CreateStudentCourseRequest request);
        public DynamicModelsResponse<StudentCourseReponse> GetStudentCourses(StudentFilter request, PagingRequest paging);
        // chỗ này fix sau
        public IList<StudentCourse> GetStudentCoursesByStudentId(StudentCourseFilter request, PagingRequest paging, int? studentId);
    }
}
