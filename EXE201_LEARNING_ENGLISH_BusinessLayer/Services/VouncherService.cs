using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Vouncher;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class VouncherService : IVouncherService
    {

        private readonly IGenericRepository<Vouncher> _repository;
        private readonly IMapper _mapper;

        private readonly IAccountService _accountService;

        public VouncherService(IGenericRepository<Vouncher> repository
                                , IMapper mapper
                                , IAccountService accountService)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public ResponseResult<VouncherReponse> CreateVouncher(CreateVouncherRequest request)
        {
            try
            {
                // Validate

                _repository.Insert(_mapper.Map<Vouncher>(request));
                _repository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<VouncherReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<VouncherReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<VouncherReponse> DeleteVouncher(int id)
        {
            try
            {
                var existedAccount = _repository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<VouncherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false,
                    };
                }

                existedAccount.Status = 0;
                _repository.UpdateById(existedAccount, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<VouncherReponse>()
                {
                    Message = Constraints.DELETE_INFO_FAILED,
                    result = false,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<VouncherReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<VouncherReponse> GetVouncher(int id)
        {
            VouncherReponse result;
            try
            {
                result = _mapper.Map<VouncherReponse>(_repository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<VouncherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<VouncherReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<VouncherReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<VouncherReponse> GetVounchers(VouncherFilter request, PagingRequest paging)
        {
            (int, IQueryable<VouncherReponse>) result;
            try
            {
                result = _repository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<VouncherReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<VouncherReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<VouncherReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<VouncherReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<VouncherReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<VouncherReponse> UpdateVouncher(UpdateVouncherRequest request, int id)
        {
            try
            {
                var existedAccount = _repository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<VouncherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Vouncher>(request);

                _repository.UpdateById(db, id);
                _repository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<VouncherReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<VouncherReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
