using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Slot;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ISlotService
    {
        public ResponseResult<SlotReponse> GetSlot(int id);
        public ResponseResult<SlotReponse> UpdateSlot(UpdateSlotRequest request, int id);
        public ResponseResult<SlotReponse> DeleteSlot(int id);
        public ResponseResult<SlotReponse> CreateSlot(CreateSlotRequest request);
        public DynamicModelsResponse<SlotReponse> GetSlots(SlotFilter request, PagingRequest paging);
    }
}
