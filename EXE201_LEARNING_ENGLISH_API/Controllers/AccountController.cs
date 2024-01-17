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

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("GetAccount/{email}")]
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

        [HttpPut("CreateAccount")]
        public ResponseResult<AccountReponse> CreateAccount([FromBody] CreateAccountRequest request)
        {
            return _service.CreateAccount(request);
        }

        [HttpPost("UpdateAccount/{email}")]
        public ResponseResult<AccountReponse> UpdateAccount([FromBody] UpdateAccountRequest request, string email)
        {
            return _service.UpdateAccount(request, email);  
        }

        [HttpDelete("DeleteAccount/{email}")]
        public ResponseResult<AccountReponse> DeleteAccount(string email)
        {
            return _service.DeleteAccount(email);
        }
    }
}
