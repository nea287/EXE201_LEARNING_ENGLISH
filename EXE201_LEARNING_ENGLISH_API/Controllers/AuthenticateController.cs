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
            return _service.login(email, password);
        }

        [HttpGet("Verify/{email}")]
        public void VerifyByCode(string email)
        {
            _service.Verify(email);
        }

        [HttpGet("SendQrCode/{email}")]
        public bool SendQrCode(string email,[FromQuery] string? qrCode)
        {
            return _service.SendQRCodeEmail(email, qrCode);    
        }
    }
}
