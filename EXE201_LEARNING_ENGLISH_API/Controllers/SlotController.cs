using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : Controller
    {
        private readonly ISlotService _service;

        public SlotController(ISlotService service)
        {
            _service = service;
        }

        [HttpGet("GetSlot/{id}")]
        public ResponseResult<SlotReponse> GetSlot(int id)
        {
            return _service.GetSlot(id);
        }

        [HttpGet("GetListSlot")]
        public DynamicModelResponse.DynamicModelsResponse<SlotReponse> GetListSlot([FromQuery] SlotFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetSlots(filter, paging);
        }

        [HttpPost("CreateSlot")]
        //[Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<SlotReponse> CreateSlot(DayOfWeek dayOfWeek,[FromBody] CreateSlotRequest request)
        {
            return _service.CreateSlot(dayOfWeek, request);
        }

        [HttpPut("UpdateSlot/{id}")]
        //[Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<SlotReponse> UpdateSlot([FromBody] UpdateSlotRequest request, int id)
        {
            return _service.UpdateSlot(request, id);
        }

        [HttpDelete("DeleteSlot/{id}")]
        //[Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<SlotReponse> DeleteSlot(int id)
        {
            return _service.DeleteSlot(id);
        }
    }
}