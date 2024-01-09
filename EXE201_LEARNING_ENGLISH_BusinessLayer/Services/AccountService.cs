using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
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
using System.Text.RegularExpressions;
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
                //Validate
                if (PhoneNumberValidate(request.PhoneNumber))
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NUMBER_PHONE_VALIDATE,
                        result = false

                    };
                }

                var existedAccount = _repository.GetByIdByString(request.Email).Result;
                if(existedAccount != null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        result = false
                        
                    };
                }

                _repository.Insert(_mapper.Map<Account>(request));
                _repository.Save();
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
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
            

        }

        public ResponseResult<AccountReponse> DeleteAccount(string email)
        {
            try
            {
                var existedAccount = _repository.GetByIdByString(email).Result;

                if(existedAccount == null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false,
                    };
                }

                existedAccount.Status = 0;
                _repository.UpdateByIdByString(existedAccount, email);
                _repository.Save();

            }catch(Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<AccountReponse> GetAccount(string email)
        {
            AccountReponse result;
            try
            {
                result = _mapper.Map<AccountReponse>(_repository.GetByIdByString(email).Result);

                if(result == null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }catch(Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountReponse> GetAccounts(AccountFilter request, PagingRequest paging)
        {
            (int, IQueryable<AccountReponse>) result;
            try
            {
                result = _repository.GetAll().ProjectTo<AccountReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<AccountReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if(result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }catch(Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<AccountReponse> UpdateAccount(UpdateAccountRequest request, string email)
        {
            try
            {
                var existedAccount = _repository.GetByIdByString(email).Result;

                if(existedAccount == null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Account>(request);
                db.Email = email;

                _repository.UpdateByIdByString(db, email);
                _repository.Save();


            }catch(Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }

        //Validate 
        //Validate Phone Number
        public bool PhoneNumberValidate(string phoneNumber)
            => Regex.IsMatch(phoneNumber, @"[^0-9]") 
            && Regex.IsMatch(phoneNumber, @"^\d{10}$");
        
    }
}
