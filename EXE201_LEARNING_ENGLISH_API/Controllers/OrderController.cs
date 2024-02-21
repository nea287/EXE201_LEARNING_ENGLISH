using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("GetOrder/{id}")]
        public ResponseResult<OrderReponse> GetOrder(int id)
        {
            return _service.GetOrder(id);
        }

        [HttpGet("GetListOrder")]
        public DynamicModelResponse.DynamicModelsResponse<OrderReponse> GetListOrder([FromQuery] OrderFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetOrders(filter, paging);
        }

        [HttpPost("CreateOrder")]
        public ResponseResult<OrderReponse> CreateOrder([FromBody] CreateOrderRequest request)
        {
            return _service.CreateOrder(request);

        }

        [HttpPut("UpdateOrder/{id}/{courseId}")]
        public ResponseResult<OrderReponse> UpdateOrder([FromBody] UpdateOrderRequest request, int id, int courseId)
        {
            return _service.UpdateOrder(request, id, courseId);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public ResponseResult<OrderReponse> DeleteOrder(int id)
        {
            return _service.DeleteOrder(id);
        }
    }
}
