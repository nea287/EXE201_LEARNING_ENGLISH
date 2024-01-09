using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetCategory/{id}")]
        public ResponseResult<CategoryReponse> GetCategory(int id)
        {
            return _service.GetCategory(id);
        }

        [HttpGet("GetListCategory")]
        public DynamicModelResponse.DynamicModelsResponse<CategoryReponse> GetListCategory([FromQuery] CategoryFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetCategorys(filter, paging);
        }

        [HttpPut("CreateCategory")]
        public ResponseResult<CategoryReponse> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            return _service.CreateCategory(request);
        }

        [HttpPost("UpdateCategory/{id}")]
        public ResponseResult<CategoryReponse> UpdateCategory([FromBody] UpdateCategoryRequest request, int id)
        {
            return _service.UpdateCategory(request, id);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public ResponseResult<CategoryReponse> DeleteCategory(int id)
        {
            return _service.DeleteCategory(id);
        }
    }
}
