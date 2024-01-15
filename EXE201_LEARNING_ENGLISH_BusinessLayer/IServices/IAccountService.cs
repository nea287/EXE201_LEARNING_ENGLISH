using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IAccountService
    {
        public ResponseResult<AccountReponse> GetAccount(string email);
        public ResponseResult<AccountReponse> UpdateAccount(UpdateAccountRequest request, string email);
        public ResponseResult<AccountReponse> DeleteAccount(string email);
        public ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request);
        public DynamicModelsResponse<AccountReponse> GetAccounts(AccountFilter request, PagingRequest paging);
        public ResponseResult<AccountReponse> Login(string email, string password);
        public ResponseResult<AccountReponse> Register(CreateAccount1Request request, string code, string codeVerify);
        public bool SendQRCodeEmail(string receiveEmail, string qrCodeData);
        public bool Verify(string mail);
        public bool RecognitionFaceId(string? unknowImage);
        public bool Logout();
    }
}
