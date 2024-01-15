using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Certificate;
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

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly IGenericRepository<Certificate> _repository;
        private readonly IMapper _mapper;

        public CertificateService(IGenericRepository<Certificate> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ResponseResult<CertificateReponse> CreateCertificate(CreateCertificateRequest request)
        {
            try
            {

                _repository.Insert(_mapper.Map<Certificate>(request));
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CertificateReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CertificateReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CertificateReponse> DeleteCertificate(int id)
        {
            try
            {
                var existedCertificate = _repository.GetByIdByInt(id).Result;

                if (existedCertificate == null || existedCertificate.Status == 0)
                {
                    return new ResponseResult<CertificateReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                existedCertificate.Status = 0;

                _repository.UpdateById(existedCertificate, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CertificateReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CertificateReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true
            };
        }

        public ResponseResult<CertificateReponse> GetCertificate(int id)
        {
            CertificateReponse result;

            try
            {
                result = _mapper.Map<CertificateReponse>(_repository.GetByIdByInt(id).Result);

                if (result == null || result.Status == 0)
                {
                    return new ResponseResult<CertificateReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<CertificateReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CertificateReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<CertificateReponse> GetCertificates(CertificateFilter request, PagingRequest paging)
        {
            (int, IQueryable<CertificateReponse>) result;
            try
            {
                result = _repository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<CertificateReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<CertificateReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<CertificateReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<CertificateReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<CertificateReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<CertificateReponse> UpdateCertificate(UpdateCertificateRequest request, int id)
        {
            try
            {
                var existedCertificate = _repository.GetByIdByInt(id).Result;

                if (UpdateCertificate == null || existedCertificate.Status == 0)
                {
                    return new ResponseResult<CertificateReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                existedCertificate.Status = request.Status;
                existedCertificate.Image = request.Image;
                existedCertificate.TeacherId = request.TeacherId;
                existedCertificate.CertificateName = request.CertificateName;

                _repository.UpdateById(existedCertificate, id);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<CertificateReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<CertificateReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }
    }
}
