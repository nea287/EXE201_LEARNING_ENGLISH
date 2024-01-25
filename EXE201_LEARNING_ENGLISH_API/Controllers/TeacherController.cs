using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet("GetTeacher/{id}")]
        public ResponseResult<TeacherReponse> GetTeacher(int id)
        {
            return _service.GetTeacher(id);
        }

        [HttpGet("GetListTeacher")]
        public DynamicModelResponse.DynamicModelsResponse<TeacherReponse> GetListTeacher([FromQuery] TeacherFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetTeachers(filter, paging);
        }

        [HttpPost("CreateTeacher")]
        public ResponseResult<TeacherReponse> CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            return _service.CreateTeacher(request);
        }

        [HttpPut("UpdateTeacher/{id}")]
        public ResponseResult<TeacherReponse> UpdateTeacher([FromBody] UpdateTeacherRequest request, int id)
        {
            return _service.UpdateTeacher(request, id);
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public ResponseResult<TeacherReponse> DeleteTeacher(int id)
        {
            return _service.DeleteTeacher(id);
        }
    }
}