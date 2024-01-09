using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
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
        public ResponseResult<AccountReponse> UpdateAccount(UpdateAccountRequest request);
        public ResponseResult<AccountReponse> DeleteAccount(string email);
        public ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request);
        public DynamicModelsResponse<AccountReponse> GetAccounts();
    }
}
