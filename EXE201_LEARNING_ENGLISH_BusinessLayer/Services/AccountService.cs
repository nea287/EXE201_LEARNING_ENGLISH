using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
using QRCoder;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using System.Text.RegularExpressions;
using System;
using System.Text;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public AccountService(IGenericRepository<Account> repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }
        public ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                //Validate
                if (PhoneNumberValidate(request.PhoneNumber))
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NUMBER_PHONE_VALIDATE,
                        result = false

                    };
                }

                var existedAccount = _repository.GetByIdByString(request.Email).Result;
                if (existedAccount != null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        result = false

                    };
                }

                _repository.Insert(_mapper.Map<Account>(request));
                _repository.Save();
            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.CREATE_INFO_FAILED,
                    result = false

                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.CREATE_INFO_SUCCESS,
                result = true
            };


        }

        public ResponseResult<AccountReponse> DeleteAccount(string email)
        {
            try
            {
                var existedAccount = _repository.GetByIdByString(email).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false,
                    };
                }

                existedAccount.Status = 0;
                _repository.UpdateByIdByString(existedAccount, email);
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.DELELTE_INFO_FAILED,
                    result = false,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.DELETE_INFO_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<AccountReponse> GetAccount(string email)
        {
            AccountReponse result;
            try
            {
                result = _mapper.Map<AccountReponse>(_repository.GetByIdByString(email).Result);

                if (result == null || result.Status == 0)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Value = result,
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountReponse> GetAccounts(AccountFilter request, PagingRequest paging)
        {
            (int, IQueryable<AccountReponse>) result;
            try
            {
                result = _repository.GetAll().Where(x => x.Status != 0)
                    .ProjectTo<AccountReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<AccountReponse>(request))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2.Count() == 0)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
                    {
                        Message = Constraints.EMPTY_INFO,
                    };
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
                {
                    Message = Constraints.LOAD_INFO_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountReponse>()
            {
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<AccountReponse> UpdateAccount(UpdateAccountRequest request, string email)
        {
            try
            {
                var existedAccount = _repository.GetByIdByString(email).Result;

                if (existedAccount == null || existedAccount.Status == 0)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.NOT_FOUND_INFO,
                        result = false
                    };
                }

                var db = _mapper.Map<Account>(request);
                db.Email = email;

                _repository.UpdateByIdByString(db, email);
                _repository.Save();


            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.UPDATE_INFO_FAILED,
                    result = false
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.UPDATE_INFO_SUCCESS,
                result = true
            };
        }

        //Validate 
        //Validate Phone Number
        public bool PhoneNumberValidate(string phoneNumber)
            => Regex.IsMatch(phoneNumber, @"[^0-9]")
            && Regex.IsMatch(phoneNumber, @"^\d{10}$");

        public ResponseResult<AccountReponse> login(string email, string password)
        {
            AccountReponse result;
            try
            {
                result = _mapper.Map<AccountReponse>(
                    _repository.Find(x => x.Email.Equals(email)
                    && x.Password.Equals(password)));

                if (result == null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        result = false,
                        Message = Constraints.NOT_FOUND_INFO
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    result = false,
                    Message = Constraints.LOGIN_FAILED,
                };
            }
            finally
            {
                lock (_repository) ;
            }
            return new ResponseResult<AccountReponse>()
            {
                result = true,
                Value = result
            };
        }

        public ResponseResult<AccountReponse> Register(CreateAccount1Request request, string code, string codeVerify)
        {
            try
            {
                if (!codeVerify.Equals(code))
                {
                    throw new Exception();
                }

                var existedAccount = _repository.GetByIdByString(request.Email).Result;
                if (existedAccount != null)
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.EXISTED_INFO,
                        result = false

                    };
                }

                _repository.Insert(_mapper.Map<Account>(request));
                _repository.Save();

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountReponse>()
                {
                    Message = Constraints.REGISTER_FAILED,
                    result = true,
                };
            }
            finally
            {
                lock (_repository) ;
            }

            return new ResponseResult<AccountReponse>()
            {
                Message = Constraints.REGISTER_SUCCESS,
                result = true,
            };
        }
        public bool SendQRCodeEmail(string receiveEmail, string? qrCodeData)
        {

            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();

                QRCodeData qrCodeDataString = qrGenerator.CreateQrCode("Your QR Code Data", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeDataString);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Tạo email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Thơ nè!", "your-email@example.com"));
                message.To.Add(new MailboxAddress("", receiveEmail));
                message.Subject = "Mã QR Code";

                // Tạo phần thân email bằng hình ảnh mã QR
                if (qrCodeData == null || qrCodeData.Equals(""))
                {
                    var builder = new BodyBuilder();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        qrCodeImage.Save(memoryStream, ImageFormat.Png);
                        memoryStream.Position = 0;

                        builder.Attachments.Add("qrcode.png", memoryStream);

                        // Lưu ý: Đối với email HTML, bạn có thể sử dụng builder.HtmlBody để nhúng mã QR dưới dạng hình ảnh trong email.

                        message.Body = builder.ToMessageBody();
                    }
                }
                else
                {

                    byte[] byteArray = Encoding.UTF8.GetBytes(qrCodeData);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        qrCodeImage.Save(memoryStream, ImageFormat.Png);
                        memoryStream.Position = 0;


                        // Lưu ý: Đối với email HTML, bạn có thể sử dụng builder.HtmlBody để nhúng mã QR dưới dạng hình ảnh trong email.
                        memoryStream.Write(byteArray, 0, byteArray.Length);
                        message.Body = MimeEntity.Load(memoryStream);
                    }
                }


                // Gửi email
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("tho.kieu@reso.vn", "1phanngocnga");

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            // Tạo mã QR từ dữ liệu qrCodeData

            return true;
        }

        public void Verify(string mail)
        {
            string code = Convert.ToString(new Random().Next(10000, 99999));

            MimeMessage message = new MimeMessage(); // tạo đối tượng mimemessage

            message.From.Add(new MailboxAddress("Kiều Thơ Nguyễn Ngọc", "tho.kieu@reso.vn"));
            message.To.Add(new MailboxAddress("Anh Anh", mail));

            message.Subject = "Mã xác thực";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = code;

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            client.Authenticate("tho.kieu@reso.vn", "1phanngocnga");

            client.Send(message);

            client.Disconnect(true);

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            var dataToCache = Encoding.UTF8.GetBytes(code);
            _cache.Set("verify", dataToCache, options);


        }
    }
}
