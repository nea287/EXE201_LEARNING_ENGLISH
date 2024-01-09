using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
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
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _repository;
        private readonly IMapper _mapper;

        public CourseService(IGenericRepository<Course> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<CourseReponse> CreateCourse(CreateCourseRequest request)
        {
            try
            {

                _repository.Insert(_mapper.Map<Course>(request));
                _repository.Save();

            }catch(Exception ex)
            {
                return new ResponseResult<CourseReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CourseReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CourseReponse> DeleteCourse(int id)
        {
            try
            {
                var existedCourse = _repository.GetByIdByInt(id).Result;

                if(existedCourse == null || existedCourse.Status == 0)
                {
                    return new ResponseResult<CourseReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                existedCourse.Status = 0;

                _repository.UpdateById(existedCourse, id);
                _repository.Save();

            } catch (Exception ex)
            {
                return new ResponseResult<CourseReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CourseReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CourseReponse> GetCourse(int id)
        {
            CourseReponse result;

            try
            {
                result = _mapper.Map<CourseReponse>(_repository.GetByIdByInt(id).Result);

                if(result == null || result.Status == 0)
                {
                    return new ResponseResult<CourseReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }catch(Exception ex)
            {
                return new ResponseResult<CourseReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CourseReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<CourseReponse> GetCourses(CourseFilter request, PagingRequest paging)
        {
            (int, IQueryable<CourseReponse>) result;
            try
            {
                result = _repository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<CourseReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<CourseReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<CourseReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<CourseReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<CourseReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<CourseReponse> UpdateCourse(UpdateCourseRequest request, int id)
        {
            try
            {
                var existedAccount = _repository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<CourseReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Course>(request);
                db.CategoryId = id;

                _repository.UpdateById(db, id);
                _repository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<CourseReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CourseReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
