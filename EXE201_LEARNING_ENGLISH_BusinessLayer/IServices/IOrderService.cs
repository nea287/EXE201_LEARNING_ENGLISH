using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IOrderService
    {
        public ResponseResult<OrderReponse> GetOrder(int id);
        public ResponseResult<OrderReponse> UpdateOrder(UpdateOrderRequest request, int id, int courseId);
        public ResponseResult<OrderReponse> DeleteOrder(int id);
        public ResponseResult<OrderReponse> CreateOrder(CreateOrderRequest request);
        public DynamicModelsResponse<OrderReponse> GetOrders(OrderFilter request, PagingRequest paging);
    }
}
