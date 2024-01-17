using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IConfiguration _configuration;

        public AuthenticateController(IAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        
        [HttpPost("Login/{email}/{password}")]
        [AllowAnonymous]
        public ResponseResult<AccountReponse> Login(string email, string password)
        {
            return _service.Login(email, password);
        }
        [HttpGet("Verify/{email}")]
        [AllowAnonymous]
        public bool VerifyByCode(string email)
        {
            return _service.Verify(email);
        }

        [HttpGet("SendQrCode/{email}")]
        [AllowAnonymous]
        public bool SendQrCode(string email,[FromQuery] string? qrCode)
        {
            return _service.SendQRCodeEmail(email, qrCode);    
        }

        [HttpGet("RecognitionFaceId")]
        [Authorize]
        public bool RecognitionFaceId([FromQuery] string? unknowImage)
        {
            return _service.RecognitionFaceId(unknowImage);
        }

        [HttpGet("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool Logout()
        {
            return _service.Logout();
        }
    }
}
