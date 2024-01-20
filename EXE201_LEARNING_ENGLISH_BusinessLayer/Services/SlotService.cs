using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
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
    public class SlotService : ISlotService
    {
        private readonly IGenericRepository<Slot> _slotRepository;
        private readonly IGenericRepository<StudentCourse> _studentCourseRepository;
        private readonly IMapper _mapper;

        public SlotService(IGenericRepository<Slot> slotRepository
                            , IGenericRepository<StudentCourse> studentCourseRepository
                            , IMapper mapper)
        {
            _slotRepository = slotRepository;
            _studentCourseRepository = studentCourseRepository;
            _mapper = mapper;
        }
        public ResponseResult<SlotReponse> CreateSlot(DayOfWeek dayOfWeek, CreateSlotRequest request)
        {
            try
            {
                var existedStudetnCourse = _studentCourseRepository.GetFirstOrDefault(x => x.StudentCourseId == request.StudentCourseId);

                if (existedStudetnCourse == null)
                {
                    return new ResponseResult<SlotReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                List<DateTime> listOfDatetime = Utils.GetDatesInRangeByDayOfWeek((DateTime) request.StartTime, (DateTime) request.EndTime, dayOfWeek);
                foreach (var date in listOfDatetime)
                {
                    request.StartTime = date;
                    request.EndTime = date.AddHours((double)request.Duration);
                    var p = _mapper.Map<Slot>(request);
                    _slotRepository.Insert(_mapper.Map<Slot>(request));
                    _slotRepository.Save();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<SlotReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_slotRepository) ;
            }

            return new ResponseResult<SlotReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<SlotReponse> DeleteSlot(int id)
        {
            try
            {
                var existedSlot = _slotRepository.GetByIdByInt(id).Result;

                if (existedSlot == null)
                {
                    return new ResponseResult<SlotReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                _slotRepository.UpdateById(existedSlot, id);
                _slotRepository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<SlotReponse>()
                {
                    Message = Constraints.DELETE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_slotRepository) ;
            }

            return new ResponseResult<SlotReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<SlotReponse> GetSlot(int id)
        {
            SlotReponse result;

            try
            {
                result = _mapper.Map<SlotReponse>(_slotRepository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<SlotReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<SlotReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_slotRepository) ;
            }

            return new ResponseResult<SlotReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<SlotReponse> GetSlots(SlotFilter request, PagingRequest paging)
        {
            (int, IQueryable<SlotReponse>) result;
            try
            {
                result = _slotRepository.GetAll()
                    .ProjectTo<SlotReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<SlotReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<SlotReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<SlotReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_slotRepository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<SlotReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<SlotReponse> UpdateSlot(UpdateSlotRequest request, int id)
        {
            try
            {
                var existedAccount = _slotRepository.GetByIdByInt(id).Result;

                if (existedAccount == null)
                {
                    return new ResponseResult<SlotReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Slot>(request);

                _slotRepository.UpdateById(db, id);
                _slotRepository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<SlotReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_slotRepository) ;
            }

            return new ResponseResult<SlotReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}

