using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.StudentCourse;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet("GetStudent/{id}")]
        public ResponseResult<StudentReponse> GetStudent(int id)
        {
            return _service.GetStudent(id);
        }

        [HttpGet("GetListStudent")]
        public DynamicModelResponse.DynamicModelsResponse<StudentReponse> GetListStudent([FromQuery] StudentFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetStudents(filter, paging);
        }

        [HttpPost("CreateStudent")]
        public ResponseResult<StudentReponse> CreateStudent([FromBody] CreateStudentRequest request)
        {
            return _service.CreateStudent(request);
        }

        [HttpPut("UpdateStudent/{id}")]
        public ResponseResult<StudentReponse> UpdateStudent([FromBody] UpdateStudentRequest request, int id)
        {
            return _service.UpdateStudent(request, id);
        }

        [HttpDelete("DeleteStudent/{id}")]
        public ResponseResult<StudentReponse> DeleteStudent(int id)
        {
            return _service.DeleteStudent(id);
        }

        [HttpPost("CreateStudentCourse")]
        public ResponseResult<StudentCourseReponse> CreateStudentCourse([FromBody] CreateStudentCourseRequest request)
        {
            return _service.CreateStudentCourse(request);
        }
    }
}
