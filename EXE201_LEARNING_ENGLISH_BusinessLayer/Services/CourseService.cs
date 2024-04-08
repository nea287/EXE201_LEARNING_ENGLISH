using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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
                #region Validate 
                CourseValidate courseValidate = new CourseValidate();

                bool resultValidate = courseValidate
                    .CheckListNumberValidate((decimal)request.Duration, 
                        (decimal)request.UnitPrice, (decimal)request.NumberOfLesson);

                if (resultValidate)
                {
                    return new ResponseResult<CourseReponse>()
                    {
                        Message = Constraints.NUMBER_INVALIDATE,
                        result = false
                    };
                }

                #endregion
                if (request.ImageFile != null && request.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        request.ImageFile.CopyTo(memoryStream);
                        request.Image = memoryStream.ToArray();
                    }
                }
                
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
                    Message = Constraints.DELETE_INFO_FAILED,
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
                result= _repository.GetAll()
                    .Where(x => x.Status != 0)
                    .ProjectTo<CourseReponse>(_mapper.ConfigurationProvider)
                    /*.DynamicFilter(_mapper.Map<CourseReponse>(request))*/
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                //result.Item1 = result1.Item1;
                //result.Item2 = _mapper.Map<IQueryable<CourseReponse>>(result1.Item2);

                //foreach(var e in result1.Item2)
                //{
                    
                //}


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
                    Size = paging.pageSize, 
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }

        public IList<Course> GetCoursesByTeacherId(int id)
        {
            IList<Course> result = null;
            try
            {
                result = _repository.GetAll(x => x.TeacherId == id).ToList();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public ResponseResult<CourseReponse> UpdateCourse(UpdateCourseRequest request, int id)
        {
            try
            {
                var existedCourse = _repository.GetByIdByInt(id).Result;

                if (UpdateCourse == null || existedCourse.Status == 0)
                {
                    return new ResponseResult<CourseReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                existedCourse.NumberOfLesson = request.NumberOfLesson;
                existedCourse.Status = request.Status;
                existedCourse.Duration = request.Duration;
                existedCourse.UnitPrice = request.UnitPrice;
                existedCourse.Description = request.Description;
                existedCourse.CategoryId = request.CategoryId;
                existedCourse.CourseName = request.CourseName;

                _repository.UpdateById(existedCourse, id);
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
