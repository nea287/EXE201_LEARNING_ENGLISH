using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.StudentCourse;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class StudentService : IStudentService
    {

        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<StudentCourse> _studentCourseRepository;
        private readonly IMapper _mapper;

        private readonly IAccountService _accountService;
        private readonly ICourseService _courseService;

        public StudentService(IGenericRepository<Student> studentRepository
                                , IGenericRepository<StudentCourse> studentCourseRepository
                                , IAccountService accountService
                                , ICourseService courseService
                                , IMapper mapper)
        {
            _studentRepository = studentRepository;
            _studentRepository = studentRepository;
            _courseService = courseService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public ResponseResult<StudentReponse> CreateStudent(CreateStudentRequest request)
        {
            try
            {
                // Validate
                // Make sure account have email 
                //if (_accountService.GetAccount(request.Email) == null)
                //{
                //    return new ResponseResult<StudentReponse>()
                //    {
                //        Message = "This email is not existed",
                //        result = false
                //    };
                //}

                // Make sure student have unique email
                //if (_studentRepository.GetByIdByString(request.Email) != null)
                //{
                //    return new ResponseResult<StudentReponse>()
                //    {
                //        Message = "This email is in used",
                //        result = false
                //    };
                //}

                _studentRepository.Insert(_mapper.Map<Student>(request));
                _studentRepository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<StudentReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_studentRepository);
            }

            return new ResponseResult<StudentReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<StudentCourseReponse> CreateStudentCourse(CreateStudentCourseRequest request)
        {
            try
            {
                // validate
                // make sure student exist
                if (_studentRepository.GetByIdByInt(request.StudentId) == null)
                {
                    return new ResponseResult<StudentCourseReponse>()
                    {
                        Message = "This student don't exist",
                        result = false
                    };
                }

                // make sure course exist
                //if (_courseService.GetCourse(request.CourseId) == null)
                //{
                //    return new ResponseResult<StudentCourseReponse>()
                //    {
                //        Message = "This course don't exist",
                //        result = false
                //    };
                //}

                _studentCourseRepository.Insert(_mapper.Map<StudentCourse>(request));
                _studentCourseRepository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<StudentCourseReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_studentRepository) ;
            }

            return new ResponseResult<StudentCourseReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<StudentReponse> DeleteStudent(int id)
        {
            try
            {
                var existedAccount = _studentRepository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<StudentReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false,
                    };
                }

                existedAccount.Status = 0;
                _studentRepository.UpdateById(existedAccount, id);
                _studentRepository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<StudentReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false,
                };
            }
            finally
            {
                lock (_studentRepository) ;
            }

            return new ResponseResult<StudentReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<StudentReponse> GetStudent(int id)
        {
            StudentReponse result;
            try
            {
                result = _mapper.Map<StudentReponse>(_studentRepository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<StudentReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<StudentReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_studentRepository);
            }

            return new ResponseResult<StudentReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<StudentCourseReponse> GetStudentCourses(StudentFilter request, PagingRequest paging)
        {
            throw new Exception();
        }

        public DynamicModelResponse.DynamicModelsResponse<StudentReponse> GetStudents(StudentFilter request, PagingRequest paging)
        {
            (int, IQueryable<StudentReponse>) result;
            try
            {
                result = _studentRepository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<StudentReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<StudentReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<StudentReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<StudentReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_studentRepository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<StudentReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<StudentReponse> UpdateStudent(UpdateStudentRequest request, int id)
        {
            try
            {
                var existedAccount = _studentRepository.GetByIdByInt(id).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<StudentReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Student>(request);

                _studentRepository.UpdateById(db, id);
                _studentRepository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<StudentReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_studentRepository) ;
            }

            return new ResponseResult<StudentReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
