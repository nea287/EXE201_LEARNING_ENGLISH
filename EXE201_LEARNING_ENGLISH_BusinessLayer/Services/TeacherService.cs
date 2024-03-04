using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher;
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
    public class TeacherService : ITeacherService
    {
        private readonly IGenericRepository<Teacher> _teacherRepository;
        private readonly IMapper _mapper;


        public TeacherService(IGenericRepository<Teacher> teacherRepository
                                , IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public ResponseResult<TeacherReponse> CreateTeacher(CreateTeacherRequest request)
        {
            try
            {
                int countTeacher = _teacherRepository.GetAll().Count();
                Teacher teacher = _mapper.Map<Teacher>(request);
                teacher.TeacherCode = "DT" + "11" + String.Concat(countTeacher);

                teacher.Status = 1;

                _teacherRepository.Insert(teacher);
                _teacherRepository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<TeacherReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new ResponseResult<TeacherReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<TeacherReponse> DeleteTeacher(int id)
        {
            try
            {
                var existedAccount = _teacherRepository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<TeacherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false,
                    };
                }

                existedAccount.Status = 0;
                _teacherRepository.UpdateById(existedAccount, id);
                _teacherRepository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<TeacherReponse>()
                {
                    Message = Constraints.DELETE_INFO_FAILED,
                    result = false,
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new ResponseResult<TeacherReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<TeacherReponse> GetTeacher(int id)
        {
            TeacherReponse result;
            try
            {
                result = _mapper.Map<TeacherReponse>(_teacherRepository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<TeacherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<TeacherReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new ResponseResult<TeacherReponse>()
            {
                Value = result,
            };
        }

        public ResponseResult<TeacherReponse> GetTeacher(string email)
        {
            TeacherReponse result;
            try
            {
                result = _mapper.Map<TeacherReponse>(_teacherRepository.GetFirstOrDefault(x => x.Email.Equals(email)));

                if (result == null)
                {
                    return new ResponseResult<TeacherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<TeacherReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new ResponseResult<TeacherReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<TeacherReponse> GetTeachers(TeacherFilter request, PagingRequest paging)
        {
            (int, IQueryable<TeacherReponse>) result;
            try
            {
                result = _teacherRepository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<TeacherReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<TeacherReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<TeacherReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<TeacherReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<TeacherReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<TeacherReponse> UpdateTeacher(UpdateTeacherRequest request, int id)
        {
            try
            {
                var existedAccount = _teacherRepository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<TeacherReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Teacher>(request);

                _teacherRepository.UpdateById(db, id);
                _teacherRepository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<TeacherReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_teacherRepository) ;
            }

            return new ResponseResult<TeacherReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
