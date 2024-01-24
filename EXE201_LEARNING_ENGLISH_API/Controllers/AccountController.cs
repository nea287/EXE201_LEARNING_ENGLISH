using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct.Security;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ISubAccountService _subService;

        public AccountController(IAccountService service, ISubAccountService subService)
        {
            _service = service;
            _subService = subService;
        }

        [HttpGet("GetAccount/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ResponseResult<AccountReponse> GetAccount(string email)
        {
            return _service.GetAccount(email);
        }

        [HttpGet("GetListAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize] Như nhau không ảnh hưởng
        public DynamicModelResponse.DynamicModelsResponse<AccountReponse> GetListAccount([FromQuery] AccountFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetAccounts(filter, paging);
        }

        [HttpPost("CreateAccount")]
        [AllowAnonymous]
        public ResponseResult<AccountReponse> CreateAccount([FromBody] CreateAccountRequest request)
        {
            return _subService.CreateAccount(request);
        }

        [HttpPut("UpdateAccount/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ResponseResult<AccountReponse> UpdateAccount([FromBody] UpdateAccountRequest request, string email)
        {
            return _service.UpdateAccount(request, email);  
        }

        [HttpDelete("DeleteAccount/{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ResponseResult<AccountReponse> DeleteAccount(string email)
        {
            return _service.DeleteAccount(email);
        }

        [HttpPost("CreateListAccountExcelFile/{filePath}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool CreateListAccountExcelFile(string filePath)
        {
            return _service.CreateListAccountExcelFile(filePath);
        }
    }
}
