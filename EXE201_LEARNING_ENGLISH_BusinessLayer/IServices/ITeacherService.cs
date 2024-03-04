using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ITeacherService
    {
        public ResponseResult<TeacherReponse> GetTeacher(int id);
        public ResponseResult<TeacherReponse> GetTeacher(string email);
        public ResponseResult<TeacherReponse> UpdateTeacher(UpdateTeacherRequest request, int id);
        public ResponseResult<TeacherReponse> DeleteTeacher(int id);
        public ResponseResult<TeacherReponse> CreateTeacher(CreateTeacherRequest request);
        public DynamicModelsResponse<TeacherReponse> GetTeachers(TeacherFilter request, PagingRequest paging);
    }
}
