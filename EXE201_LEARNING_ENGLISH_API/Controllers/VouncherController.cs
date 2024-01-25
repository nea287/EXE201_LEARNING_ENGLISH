using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Vouncher;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [EnableCors("AllowAnyOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class VouncherController : Controller
    {
        private readonly IVouncherService _service;

        public VouncherController(IVouncherService service)
        {
            _service = service;
        }

        [HttpGet("GetVouncher/{id}")]
        public ResponseResult<VouncherReponse> GetVouncher(int id)
        {
            return _service.GetVouncher(id);
        }

        [HttpGet("GetListVouncher")]
        public DynamicModelResponse.DynamicModelsResponse<VouncherReponse> GetListVouncher([FromQuery] VouncherFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetVounchers(filter, paging);
        }

        [HttpPost("CreateVouncher")]
        public ResponseResult<VouncherReponse> CreateVouncher([FromBody] CreateVouncherRequest request)
        {
            return _service.CreateVouncher(request);
        }

        [HttpPut("UpdateVouncher/{id}")]
        public ResponseResult<VouncherReponse> UpdateVouncher([FromBody] UpdateVouncherRequest request, int id)
        {
            return _service.UpdateVouncher(request, id);
        }

        [HttpDelete("DeleteVouncher/{id}")]
        public ResponseResult<VouncherReponse> DeleteVouncher(int id)
        {
            return _service.DeleteVouncher(id);
        }
    }
}