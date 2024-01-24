using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly ISubCourseService _subService;

        public CourseController(ICourseService service, ISubCourseService subService)
        {
            _service = service;
            _subService = subService;
        }

        [HttpGet("GetCourse/{id}")]
        [AllowAnonymous]
        public ResponseResult<CourseReponse> GetCourse(int id)
        {
            return _service.GetCourse(id);
        }

        [HttpGet("GetListCourse")]
        [AllowAnonymous]
        public DynamicModelResponse.DynamicModelsResponse<CourseReponse> GetListCourse([FromQuery] CourseFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetCourses(filter, paging);
        }

        [HttpPost("CreateCourse")]
        public ResponseResult<CourseReponse> CreateCourse([FromBody] CreateCourseRequest request)
        {
            return _service.CreateCourse(request);
        }

        [HttpPut("UpdateCourse/{id}")]
        public ResponseResult<CourseReponse> UpdateCourse([FromBody] UpdateCourseRequest request, int id)
        {
            return _service.UpdateCourse(request, id);
        }

        [HttpDelete("DeleteCourse/{id}")]
        public ResponseResult<CourseReponse> DeleteCourse(int id)
        {
            return _service.DeleteCourse(id);
        }

        [HttpGet("GetListEmailOfCourse/{courseId}")]
        public ICollection<string> GetListEmailOfCourse(int courseId)
        {
            return _subService.GetListEmailOfStudentsInCourse(courseId);
        }

        [HttpPost("SendListEmailToTeacher/{courseId}")]
        public bool SendListEmailToTeacher(int courseId)
        {
            return _subService.SendListEmailOfCourseToTeacher(courseId);
        }
    }
}
