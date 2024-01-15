using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountService _service;

        public AuthenticateController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("Login/{email}/{password}")]
        public ResponseResult<AccountReponse> Login(string email, string password)
        {
            return _service.Login(email, password);
        }

        [HttpGet("Verify/{email}")]
        public bool VerifyByCode(string email)
        {
            return _service.Verify(email);
        }

        [HttpGet("SendQrCode/{email}")]
        public bool SendQrCode(string email,[FromQuery] string? qrCode)
        {
            return _service.SendQRCodeEmail(email, qrCode);    
        }

        [HttpGet("RecognitionFaceId")]
        public bool RecognitionFaceId([FromQuery] string? unknowImage)
        {
            return _service.RecognitionFaceId(unknowImage);
        }

        [HttpGet("Logout")]
        public bool Logout()
        {
            return _service.Logout();
        }
    }
}
