using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Vouncher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface IVouncherService
    {
        public ResponseResult<VouncherReponse> GetVouncher(int id);
        public ResponseResult<VouncherReponse> UpdateVouncher(UpdateVouncherRequest request, int id);
        public ResponseResult<VouncherReponse> DeleteVouncher(int id);
        public ResponseResult<VouncherReponse> CreateVouncher(CreateVouncherRequest request);
        public DynamicModelsResponse<VouncherReponse> GetVounchers(VouncherFilter request, PagingRequest paging);
    }
}
