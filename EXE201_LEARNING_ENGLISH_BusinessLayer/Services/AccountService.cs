using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly IMapper _mapper;

        public AccountService(IGenericRepository<Account> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                var existedAccount = _repository.GetByIdByString(request.Email).Result;
                if(existedAccount != null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        
                    };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.EXISTED_INFO,

            };
            

        }

        public ResponseResult<AccountReponse> DeleteAccount(string email)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<AccountReponse> GetAccount(string email)
        {
            throw new NotImplementedException();
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountReponse> GetAccounts(AccountFilter request, PagingRequest paging)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<AccountReponse> UpdateAccount(UpdateAccountRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
