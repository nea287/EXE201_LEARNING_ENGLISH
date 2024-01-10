using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenericRepository<EXE201_LEARNING_ENGLISH_DataLayer.Models.OrderDetail> _repository;
        private readonly IMapper _mapper;

        public OrderDetailService(IGenericRepository<EXE201_LEARNING_ENGLISH_DataLayer.Models.OrderDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<OrderDetailReponse> CreateOrderDetail(CreateOrderDetailRequest request)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrderDetailInOrder(EXE201_LEARNING_ENGLISH_DataLayer.Models.OrderDetail request)
        {
            try
            {
                _repository.Insert(request);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public ResponseResult<OrderDetailReponse> DeleteOrderDetail(OrderDetailRequest id)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<OrderDetailReponse> GetOrderDetail(OrderDetailRequest id)
        {
            throw new NotImplementedException();
        }

        public DynamicModelResponse.DynamicModelsResponse<OrderDetailReponse> GetOrderDetails(OrderDetailFilter request, PagingRequest paging)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<OrderDetailReponse> UpdateOrderDetail(UpdateOrderDetailRequest request, OrderDetailRequest id)
        {
            throw new NotImplementedException();
        }
    }
}
