using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class SubAccountService : AccountService, ISubAccountService
    {
        private IGenericRepository<Account> _accountRepository;
        private readonly IMapper _mapper;
        public SubAccountService(IGenericRepository<Account> repository, IMapper mapper, IDistributedCache cache, IConfiguration configuration, IRefreshTokenService refreshTokenService, IGenericRepository<FcmToken> tokenRepository) : base(repository, mapper, cache, configuration, refreshTokenService, tokenRepository)
        {
            _accountRepository = repository;
            _mapper = mapper;
        }

        public virtual ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                #region Validate
                AccountValidate accountValidate = new AccountValidate();
                string validate = accountValidate.CheckValidate(request);

                if (!validate.Equals(Constraints.VALIDATE))
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = validate,
                        result = false

                    };
                }
                #endregion

                var existedAccount = _accountRepository.GetByIdByString(request.Email).Result;
                if (existedAccount != null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        result = false

                    };
                }

                var account = _mapper.Map<Account>(request);

                switch(account.Role)
                {
                    case (int)AccountRole.STUDENT:
                        Student student = new Student()
                        {
                            Email = account.Email,
                            Status = 1,
                            StudentName = account.Email,
                        };

                        account.Students.Add(student);

                        break;

                    case (int)AccountRole.TEACHER:

                        Teacher teacher = new Teacher()
                        {
                            Email = account.Email,
                            Status = 1,
                            TeacherName = account.Email,
                        };

                        account.Teachers.Add(teacher);

                        break;
                }


                _accountRepository.Insert(account);

                
                _accountRepository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false

                };
            }
            finally
            {
                lock (_accountRepository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
