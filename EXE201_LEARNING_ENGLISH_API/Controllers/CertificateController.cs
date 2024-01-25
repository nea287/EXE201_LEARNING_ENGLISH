using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Certificate;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _service;

        public CertificateController(ICertificateService service)
        {
            _service = service;
        }

        [HttpGet("GetCertificate/{id}")]
        public ResponseResult<CertificateReponse> GetCertificate(int id)
        {
            return _service.GetCertificate(id);
        }

        [HttpGet("GetListCertificate")]
        public DynamicModelResponse.DynamicModelsResponse<CertificateReponse> GetListCertificate([FromQuery] CertificateFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetCertificates(filter, paging);
        }

        [HttpPost("CreateCertificate")]
        [Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<CertificateReponse> CreateCertificate([FromBody] CreateCertificateRequest request)
        {
            return _service.CreateCertificate(request);
        }

        [HttpPut("UpdateCertificate/{id}")]
        [Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<CertificateReponse> UpdateCertificate([FromBody] UpdateCertificateRequest request, int id)
        {
            return _service.UpdateCertificate(request, id);
        }

        [HttpDelete("DeleteCertificate/{id}")]
        [Authorize(Policy = "RequireTeacherRole")]
        public ResponseResult<CertificateReponse> DeleteCertificate(int id)
        {
            return _service.DeleteCertificate(id);
        }
    }
}
