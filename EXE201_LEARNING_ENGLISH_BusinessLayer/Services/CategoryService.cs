using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<OrderDetail> _orderRepository;
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IMapper mapper, IGenericRepository<OrderDetail> orderRepository)
        {
            _orderRepository = orderRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public ResponseResult<ICollection<CategoryReponse>> CategoryStatistics()
        {
            ICollection<CategoryReponse> lstCate = new List<CategoryReponse>();

            try
            {

                lstCate = _mapper.Map<ICollection<CategoryReponse>>(_repository.GetAll().ToList());
                lstCate = lstCate.OrderByDescending(x => x.TotalAmount).ToList();


            }
            catch(Exception ex)
            {
                return new ResponseResult<ICollection<CategoryReponse>>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }

            return new ResponseResult<ICollection<CategoryReponse>>()
            {
                Value = lstCate,
                result = true,
            };



        }
        public ResponseResult<CategoryReponse> CreateCategory(CreateCategoryRequest request)
        {
            try
            {

                _repository.Insert(_mapper.Map<Category>(request));
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CategoryReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CategoryReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CategoryReponse> DeleteCategory(int id)
        {
            try
            {
                var existedCategory = _repository.GetByIdByInt(id).Result;

                if (existedCategory == null)
                {
                    return new ResponseResult<CategoryReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                _repository.HardDelete(id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CategoryReponse>()
                {
                    Message = Constraints.DELETE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CategoryReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CategoryReponse> GetCategory(int id)
        {
            CategoryReponse result;

            try
            {
                result = _mapper.Map<CategoryReponse>(_repository.GetByIdByInt(id).Result);

                if (result == null )
                {
                    return new ResponseResult<CategoryReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<CategoryReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CategoryReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<CategoryReponse> GetCategorys(CategoryFilter request, PagingRequest paging)
        {
            (int, IQueryable<CategoryReponse>) result;
            try
            {
                result = _repository.GetAll()
                    .ProjectTo<CategoryReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<CategoryReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.ToList().Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<CategoryReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<CategoryReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<CategoryReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<CategoryReponse> UpdateCategory(UpdateCategoryRequest request, int id)
        {
            try
            {
                var existedCategory = _repository.GetByIdByInt(id).Result;

                if (UpdateCategory == null)
                {
                    return new ResponseResult<CategoryReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

               existedCategory.CategoryName = request.CategoryName;

                _repository.UpdateById(existedCategory, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CategoryReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CategoryReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
