using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Feedback;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IFeedbackService
    {
        public ResponseResult<FeedbackReponse> GetFeedback(int id);
        public ResponseResult<FeedbackReponse> UpdateFeedback(UpdateFeedbackRequest request, int id);
        public ResponseResult<FeedbackReponse> DeleteFeedback(int id);
        public ResponseResult<FeedbackReponse> CreateFeedback(CreateFeedbackRequest request);
        public DynamicModelsResponse<FeedbackReponse> GetFeedbacks(FeedbackFilter request, PagingRequest paging, int slotId);
    }
}
