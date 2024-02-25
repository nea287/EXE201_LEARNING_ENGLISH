using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ICategoryService
    {
        public ResponseResult<CategoryReponse> GetCategory(int id);
        public ResponseResult<ICollection<CategoryReponse>> CategoryStatistics();
        public ResponseResult<CategoryReponse> UpdateCategory(UpdateCategoryRequest request, int id);
        public ResponseResult<CategoryReponse> DeleteCategory(int id);
        public ResponseResult<CategoryReponse> CreateCategory(CreateCategoryRequest request);
        public DynamicModelsResponse<CategoryReponse> GetCategorys(CategoryFilter request, PagingRequest paging);
    }
}
