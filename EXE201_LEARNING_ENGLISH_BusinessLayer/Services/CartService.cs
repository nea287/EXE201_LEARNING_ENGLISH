using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Cart;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class CartService : ICartService
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Course> _repository;

        public CartService(MongoDBContext context, IMapper mapper, IGenericRepository<Course> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }
        public ResponseResult<CartReponse> DeleteCart(int courseId)
        {
            try
            {
                bool checkCourse = _repository.GetByIdByInt(courseId).Result != null;

                if (!checkCourse)
                {
                    return new ResponseResult<CartReponse>()
                    {
                        result = false,
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

                FilterDefinition<Cart> filter = Builders<Cart>.Filter.Where(x => x.CourseId == courseId);
                _context.Carts.FindOneAndDelete(filter);

            }catch(Exception ex)
            {
                return new ResponseResult<CartReponse>()
                {
                    Message = Constraints.DELETE_INFO_FAILED,
                    result = false
                };
            }

            return new ResponseResult<CartReponse>()
            {
                result = true
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<CartReponse> GetCart(CartFilter filter, PagingRequest paging)
        {
            (int, IQueryable<CartReponse>) result;

            #region ussigned
            result.Item1 = 0;
            result.Item2 = new List<CartReponse>().AsQueryable();
            #endregion

            try
            {
                var data = _context.Carts.Find(_ => true).ToList().AsQueryable();

                if (data.IsNull())
                {
                    throw new Exception();
                }

                result = data.ProjectTo<CartReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<CartReponse>(filter))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

            }catch(Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<CartReponse>()
                {
                    Message = Constraints.EMPTY_INFO
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<CartReponse>()
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

        public ResponseResult<CartReponse> GetCourse(int courseId)
        {
            CartReponse cart = new CartReponse();
            try
            {
                cart = _mapper.Map<CartReponse>(
                    _context.Carts.Find(x => x.CourseId == courseId).FirstOrDefault());

                if(cart == null)
                {
                    throw new Exception();
                }

            }catch(Exception ex)
            {
                return new ResponseResult<CartReponse>()
                {
                    Message = Constraints.EMPTY_INFO,
                };
            }

            return new ResponseResult<CartReponse>()
            {
                Value = cart,
                result = true
            };
        }

        public ResponseResult<CartReponse> InsertCart(CartRequest request)
        {
            Cart result = new Cart();
            try
            {

                var data = _context.Carts.Find(x => x.CourseId == request.CourseId).FirstOrDefault();

                if(data != null)
                {
                    return new ResponseResult<CartReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        result = false
                    };
                }

                Course course = _repository.GetByIdByInt(request.CourseId).Result;

                result = new Cart()
                {
                    CourseId = request.CourseId,
                    CourseName = course.CourseName,
                    Price = course.UnitPrice.Value,
                };

                _context.Carts.InsertOne(result);

            }catch(Exception ex)
            {
                return new ResponseResult<CartReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }

            return new ResponseResult<CartReponse>()
            {
                result = true,
                Value = _mapper.Map<CartReponse>(result)
            };

        }

        public ResponseResult<CartReponse> UpdateCart(CartRequest request)
        {
            var result = new Cart();

            try
            {

                Course course = _repository.GetByIdByInt(request.CourseId).Result;

                if (course == null)
                {
                    return new ResponseResult<CartReponse>()
                    {
                        result = false,
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

                FilterDefinition<Cart> filter = Builders<Cart>.Filter.Where(x => x.CourseId == request.CourseId);
                
                if(filter == null)
                {
                    return new ResponseResult<CartReponse>()
                    {
                        result = false,
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }
                result = new Cart()
                {
                    CourseId = request.CourseId,
                    CourseName = course.CourseName,
                    Price = course.UnitPrice.Value
                };

                UpdateDefinition<Cart> update = Builders<Cart>.Update.Set(x => x, result);

                _context.Carts.FindOneAndUpdate(filter, update);


            }catch(Exception ex)
            {
                return new ResponseResult<CartReponse>()
                {
                    result = false,
                    Message = Constraints.UPDATE_INFO_FAILED,
                };
            }

            return new ResponseResult<CartReponse>()
            {
                result = true,
                Value = _mapper.Map<CartReponse>(result)
            };
        }
    }
}
