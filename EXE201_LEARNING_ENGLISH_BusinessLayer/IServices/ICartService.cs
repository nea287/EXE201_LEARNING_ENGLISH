using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Cart;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ICartService
    {
        public ResponseResult<CartReponse> GetCourse(int courseId);
        public ResponseResult<CartReponse> InsertCart(CartRequest request);
        public ResponseResult<CartReponse> UpdateCart(CartRequest request);
        public ResponseResult<CartReponse> DeleteCart(int courseId);
        public DynamicModelResponse.DynamicModelsResponse<CartReponse> GetCart(CartFilter filter, PagingRequest paging);

    }
}
