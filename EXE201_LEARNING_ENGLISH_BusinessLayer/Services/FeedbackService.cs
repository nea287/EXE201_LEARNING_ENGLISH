using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Feedback;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IGenericRepository<Feedback> _repository;
        private readonly IMapper _mapper;

        public FeedbackService(IGenericRepository<Feedback> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<FeedbackReponse> CreateFeedback(CreateFeedbackRequest request)
        {
            try
            {

                _repository.Insert(_mapper.Map<Feedback>(request));
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<FeedbackReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<FeedbackReponse> DeleteFeedback(int id)
        {
            try
            {
                var existedFeedback = _repository.GetByIdByInt(id).Result;

                if (existedFeedback == null)
                {
                    return new ResponseResult<FeedbackReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                _repository.UpdateById(existedFeedback, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<FeedbackReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<FeedbackReponse> GetFeedback(int id)
        {
            FeedbackReponse result;

            try
            {
                result = _mapper.Map<FeedbackReponse>(_repository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<FeedbackReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<FeedbackReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<FeedbackReponse> GetFeedbacks(FeedbackFilter request, PagingRequest paging, int slotId)
        {
            (int, IQueryable<FeedbackReponse>) result;
            try
            {
                result = _repository.GetAll().Where(x => x.SlotId == slotId)
                    .ProjectTo<FeedbackReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<FeedbackReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<FeedbackReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FeedbackReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<FeedbackReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<FeedbackReponse> UpdateFeedback(UpdateFeedbackRequest request, int id)
        {
            try
            {
                var existedAccount = _repository.GetByIdByInt(id).Result;

                if (existedAccount == null)
                {
                    return new ResponseResult<FeedbackReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Feedback>(request);

                _repository.UpdateById(db, id);
                _repository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<FeedbackReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<FeedbackReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}

