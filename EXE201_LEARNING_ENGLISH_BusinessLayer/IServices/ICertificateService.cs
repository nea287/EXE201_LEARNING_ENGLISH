using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Certificate;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ICertificateService
    {
        public ResponseResult<CertificateReponse> GetCertificate(int id);
        public ResponseResult<CertificateReponse> UpdateCertificate(UpdateCertificateRequest request, int id);
        public ResponseResult<CertificateReponse> DeleteCertificate(int id);
        public ResponseResult<CertificateReponse> CreateCertificate(CreateCertificateRequest request);
        public DynamicModelsResponse<CertificateReponse> GetCertificates(CertificateFilter request, PagingRequest paging);
    }
}
