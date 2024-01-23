using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Feedback;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpGet("GetFeedback/{id}")]
        public ResponseResult<FeedbackReponse> GetFeedback(int id)
        {
            return _service.GetFeedback(id);
        }

        [HttpGet("GetListFeedback")]
        public DynamicModelResponse.DynamicModelsResponse<FeedbackReponse> GetListFeedback([FromQuery] FeedbackFilter filter, [FromQuery] PagingRequest paging, int slotId)
        {
            return _service.GetFeedbacks(filter, paging, slotId);
        }

        [HttpPost("CreateFeedback")]
        public ResponseResult<FeedbackReponse> CreateFeedback([FromBody] CreateFeedbackRequest request)
        {
            return _service.CreateFeedback(request);
        }

        //[HttpPost("UpdateFeedback/{id}")]
        //public ResponseResult<FeedbackReponse> UpdateFeedback([FromBody] UpdateFeedbackRequest request, int id)
        //{
        //    return _service.UpdateFeedback(request, id);
        //}

        //[HttpDelete("DeleteFeedback/{email}")]
        //public ResponseResult<FeedbackReponse> DeleteFeedback(int id)
        //{
        //    return _service.DeleteFeedback(id);
        //}
    }
}
