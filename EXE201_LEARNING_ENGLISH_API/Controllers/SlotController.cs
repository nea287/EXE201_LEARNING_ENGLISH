using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
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

        [HttpPut("CreateSlot")]
        public ResponseResult<SlotReponse> CreateSlot([FromBody] CreateSlotRequest request)
        {
            return _service.CreateSlot(request);
        }

        [HttpPost("UpdateSlot/{id}")]
        public ResponseResult<SlotReponse> UpdateSlot([FromBody] UpdateSlotRequest request, int id)
        {
            return _service.UpdateSlot(request, id);
        }

        [HttpDelete("DeleteSlot/{id}")]
        public ResponseResult<SlotReponse> DeleteSlot(int id)
        {
            return _service.DeleteSlot(id);
        }
    }
}