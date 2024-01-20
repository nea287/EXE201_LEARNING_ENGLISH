using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Cart;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet("GetCart")]
        public DynamicModelResponse.DynamicModelsResponse<CartReponse> GetCart([FromQuery] CartFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetCart(filter, paging);
        }

        [HttpGet("GetCourse/{courseId}")]
        public ResponseResult<CartReponse> GetCourse(int courseId)
        {
            return _service.GetCourse(courseId);
        }

        [HttpDelete("DeleteCart/{courseId}")]
        public ResponseResult<CartReponse> DeleteCart(int courseId)
        {
            return _service.DeleteCart(courseId);
        }

        [HttpPost("UpdateCart")]
        public ResponseResult<CartReponse> UpdateCart([FromQuery]CartRequest request)
        {
            return _service.UpdateCart(request);
        }

        [HttpPut("CreateCart")]
        public ResponseResult<CartReponse> CreateCart([FromQuery] CartRequest request)
        {
            return _service.InsertCart(request);
        }


    }
}
