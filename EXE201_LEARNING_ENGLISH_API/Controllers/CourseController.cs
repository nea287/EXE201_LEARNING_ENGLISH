﻿using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet("GetCourse/{id}")]
        public ResponseResult<CourseReponse> GetCourse(int id)
        {
            return _service.GetCourse(id);
        }

        [HttpGet("GetListCourse")]
        public DynamicModelResponse.DynamicModelsResponse<CourseReponse> GetListCourse([FromQuery] CourseFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetCourses(filter, paging);
        }

        [HttpPut("CreateCourse")]
        public ResponseResult<CourseReponse> CreateCourse([FromBody] CreateCourseRequest request)
        {
            return _service.CreateCourse(request);
        }

        [HttpPost("UpdateCourse/{id}")]
        public ResponseResult<CourseReponse> UpdateCourse([FromBody] UpdateCourseRequest request, int id)
        {
            return _service.UpdateCourse(request, id);
        }

        [HttpDelete("DeleteCourse/{id}")]
        public ResponseResult<CourseReponse> DeleteCourse(int id)
        {
            return _service.DeleteCourse(id);
        }
    }
}