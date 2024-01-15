using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
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
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail;
using System.Collections;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IMapper _mapper;
        private readonly IOrderDetailService _orderDetailService;

        public OrderService(IGenericRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //_orderDetailService = orderDetailService;
        }
        public ResponseResult<OrderReponse> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                #region Validate
                OrderValidate orderValidate = new OrderValidate();
                bool resultValidate = orderValidate.CheckListNumberValidate(request.Quantity, 
                    request.TotalAmount.Value, (decimal)request.Discount, request.FinalAmount.Value);

                if (resultValidate)
                {
                    return new ResponseResult<OrderReponse>()
                    {
                        Message = Constraints.NUMBER_INVALIDATE,
                        result = false
                    };
                }
                #endregion

                var orderReponse = _mapper.Map<Order>(request);
                _repository.Insert(orderReponse);

                bool resultCreateOD = true;
                foreach(var order in request.OrderDetails)
                {
                    OrderDetail orderDetail = _mapper.Map<OrderDetail>(order);
                    resultCreateOD = _orderDetailService.CreateOrderDetailInOrder(orderDetail);
                }

                if (!resultCreateOD)
                {
                    throw new Exception();
                }

                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<OrderReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<OrderReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<OrderReponse> DeleteOrder(int id)
        {
            try
            {
                var existedOrder = _repository.GetByIdByInt(id).Result;

                if (existedOrder == null)
                {
                    return new ResponseResult<OrderReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                _repository.HardDelete( id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<OrderReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<OrderReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<OrderReponse> GetOrder(int id)
        {
            OrderReponse result;

            try
            {
                result = _mapper.Map<OrderReponse>(_repository.GetByIdByInt(id).Result);

                if (result == null)
                {
                    return new ResponseResult<OrderReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<OrderReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<OrderReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<OrderReponse> GetOrders(OrderFilter request, PagingRequest paging)
        {
            (int, IQueryable<OrderReponse>) result;
            try
            {
                result = _repository.GetAll()
                    .ProjectTo<OrderReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<OrderReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<OrderReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<OrderReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<OrderReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<OrderReponse> UpdateOrder(UpdateOrderRequest request, int id, int courseId)
        {
            try
            {
                var existedOrder = _repository.GetByIdByInt(id).Result;

                if (UpdateOrder == null)
                {
                    return new ResponseResult<OrderReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                existedOrder.ApproveDate = request.ApproveDate;
                existedOrder.CheckInDate = request.CheckInDate;
                existedOrder.VouncherId = request.VouncherId;
                existedOrder.Discount = request.Discount;
                existedOrder.FinalAmount = request.FinalAmount;
                existedOrder.TotalAmount = request.TotalAmount;

                ICollection<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach(var orderDetail in request.OrderDetails)
                {
                    OrderDetail data = new OrderDetail()
                    {
                        Discount = orderDetail.Discount,
                        OrderDate = orderDetail.OrderDate,
                        CourseId = courseId,
                        FinalPrice = orderDetail.FinalPrice,
                        UnitPrice = orderDetail.UnitPrice,
                    };

                    lstOrderDetail.Add(data);
                }

                existedOrder.OrderDetails = lstOrderDetail;

                _repository.UpdateById(existedOrder, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<OrderReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<OrderReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
