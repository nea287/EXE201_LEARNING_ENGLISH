using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.OrderDetail;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IOrderDetailService
    {
        public ResponseResult<OrderDetailReponse> GetOrderDetail(OrderDetailRequest id);
        public ResponseResult<OrderDetailReponse> UpdateOrderDetail(UpdateOrderDetailRequest request, OrderDetailRequest id);
        public ResponseResult<OrderDetailReponse> DeleteOrderDetail(OrderDetailRequest id);
        public ResponseResult<OrderDetailReponse> CreateOrderDetail(CreateOrderDetailRequest request);
        public DynamicModelsResponse<OrderDetailReponse> GetOrderDetails(OrderDetailFilter request, PagingRequest paging);
        public bool CreateOrderDetailInOrder(OrderDetail request);
    }
}
