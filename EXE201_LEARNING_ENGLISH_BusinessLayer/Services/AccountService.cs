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
using Newtonsoft.Json;
using MailKit.Net.Smtp;
using System.Text;
using FaceRecognitionDotNet;
using ImageFormat = System.Drawing.Imaging.ImageFormat;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers.Validate;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using XAct;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<FcmToken> _tokenRepository;
        private static List<string> _blackListedToken = new List<string>();

        public AccountService(IGenericRepository<Account> repository,
            IMapper mapper, IDistributedCache cache, IConfiguration configuration,
            IRefreshTokenService refreshTokenService, IGenericRepository<FcmToken> tokenRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _refreshTokenService = refreshTokenService;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }

        #region Create
        public ResponseResult<AccountReponse> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                #region Validate
                AccountValidate accountValidate = new AccountValidate();
                string validate = accountValidate.CheckValidate(request);

                if (!validate.Equals(Constraints.VALIDATE))
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = validate,
                        result = false

                    };
                }
                #endregion

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
        #endregion

        #region Delete
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
                    Message = Constraints.DELETE_INFO_FAILED,
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
        #endregion

        #region Get
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
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }
        #endregion

        #region Update
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
        #endregion

        #region Authenticate
        public ResponseResult<AccountReponse> Login(string email, string password)
        {
            AccountReponse result;
            try
            {
                result = _mapper.Map<AccountReponse>(
                    _repository.Find(x => x.Email.Equals(email)
                    && x.Password.Equals(password)));

                if (result.IsNull())
                {
                    return new ResponseResult<AccountReponse>()
                    {
                        Message = Constraints.LOGIN_FAILED
                    };
                }
                var checkExistedToken = CheckExistedToken(email);

                var dataToken = GenerateTokens(email, result.Role.Value);


                if (checkExistedToken != null)
                {
                    checkExistedToken.Active = true;
                    checkExistedToken.CreatedAt = DateTime.Now;
                    checkExistedToken.CreatedBy = email;
                    checkExistedToken.Email = email;
                    checkExistedToken.Fcmtoken1 = dataToken.accessToken;
                    checkExistedToken.RefeshToken = dataToken.refreshToken;
                    checkExistedToken.UpdatedBy = email;
                    checkExistedToken.UpdatedAt = DateTime.Now;

                    _tokenRepository.UpdateById(checkExistedToken, checkExistedToken.Id);
                }

                else
                {
                    FcmToken tokenToDB = new FcmToken()
                    {
                        Active = true,
                        CreatedAt = DateTime.Now,
                        CreatedBy = email,
                        Email = email,
                        Fcmtoken1 = dataToken.accessToken,
                        RefeshToken = dataToken.refreshToken,
                        UpdatedAt = DateTime.Now,
                        UpdatedBy = email,
                    };
                    _tokenRepository.Insert(tokenToDB);
                }
                    
                
                _tokenRepository.Save();

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

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1))
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));

            string dataAsync = JsonConvert.SerializeObject(result);
            var dataToCache = Encoding.UTF8.GetBytes(dataAsync);
            _cache.Set(result.Email.ToLower() + "-account", dataToCache, options);

            var emailToCache = Encoding.UTF8.GetBytes(email);
            _cache.Set("-email", emailToCache, options);

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

                QRCodeData qrCodeDataString = qrGenerator.CreateQrCode("https://www.youtube.com/watch?v=OrRf3tO8r3U", QRCodeGenerator.ECCLevel.Q);
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

        public bool Verify(string mail)
        {
            DistributedCacheEntryOptions options1 = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            var email = Encoding.UTF8.GetBytes(mail);

            _cache.Set("-mail", email, options1);

            string code = Convert.ToString(new Random().Next(10000, 99999));
            try
            {
                MimeMessage message = new MimeMessage(); // tạo đối tượng mimemessage

                message.From.Add(new MailboxAddress("Thơ nè!", "tho.kieu@reso.vn"));
                message.To.Add(new MailboxAddress("Khách hàng thân thương!", mail));

                message.Subject = "Mã xác thực";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Mã xác thực: " + code;

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
            catch (Exception ex)
            {
                throw new Exception();
            }

            return true;

        }

        public bool RecognitionFaceId(string? unknowImage)
        {
            //start compare two Image
            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Directory.GetParent(currentDirectory).FullName;


            string imageName = Encoding.UTF8.GetString(_cache.Get("-email"));
            imageName = imageName.Substring(0, imageName.Length - 4) + ".jpg";

            string imagePath = Path.Combine(parentDirectory,
                "EXE201_LEARNING_ENGLISH_BusinessLayer", "Image", "RegisterAvatar", imageName);
            var registeredImage = FaceRecognition.LoadImageFile(imagePath);

            string UnknowImagePath = "";
            if (unknowImage == null || unknowImage.Equals(""))
            {
                UnknowImagePath = Path.Combine(parentDirectory, "EXE201_LEARNING_ENGLISH_BusinessLayer", "Image", "UnknowAttendance", "avatar2.jpg");
            }
            else
            {
                UnknowImagePath = Path.Combine(parentDirectory, "EXE201_LEARNING_ENGLISH_BusinessLayer", "Image", "UnknowAttendance", unknowImage);
            }

            var unknownImage = FaceRecognition.LoadImageFile(UnknowImagePath);

            imagePath = Path.Combine(parentDirectory, "EXE201_LEARNING_ENGLISH_BusinessLayer", "dlib-models");

            // Tạo một phiên bản của lớp FaceRecognition
            using (var faceRecognition = FaceRecognition.Create(imagePath + "\\"))
            {
                // Tạo một danh sách các ảnh đã đăng ký
                var registeredImages = new[]
            {
                registeredImage
            };

                // Tạo một danh sách nhãn tương ứng với các ảnh đã đăng ký
                var labels = new[]
                {
                "Người 1"
            };

                foreach (var image in registeredImages)
                {
                    // Train the face recognition model with the registered images
                    var encodings = faceRecognition.FaceEncodings(image);

                    // Detect faces in the unknown image
                    var unknownEncodings = faceRecognition.FaceEncodings(unknownImage);
                    var faceLocations = faceRecognition.FaceLocations(unknownImage);

                    foreach (var unknownEncoding in unknownEncodings)
                    {
                        // Compare the registered faces with the detected face in the unknown image
                        var matchesDistances = FaceRecognition.FaceDistances(encodings, unknownEncoding);

                        foreach (var match in matchesDistances)
                        {
                            // Determine the corresponding person for the recognized face


                            if (match < 0.6) // Threshold for comparison
                            {
                                var label = labels;

                            }
                            else
                            {
                                try
                                {
                                    if (File.Exists(UnknowImagePath))
                                    {
                                        File.Delete(UnknowImagePath);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception();

                                }
                                return false;

                            }
                        }
                    }
                }
                try
                {
                    if (File.Exists(UnknowImagePath))
                    {
                        File.Delete(UnknowImagePath);
                    }
                }
                catch (Exception ex)
                {

                }

                return true;
            }
            //End compare two Image
        }

        public bool Logout()
        {
            try
            {
                string email = Encoding.UTF8.GetString(_cache.Get("-email"));
                _cache.Remove("-email");

                _cache.Remove(email + "-account");

                var data = _tokenRepository.Find(x => x.Email.Equals(email));
                data.Active = false;
                _tokenRepository.UpdateByIdByString(data, email);
                _tokenRepository.Save();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                lock (_repository) ;
            }

            return true;
        }
        #endregion

        #region Token
        public (string accessToken, string refreshToken) GenerateTokens(string email, int role)
        {
            string roleName = "";
            switch (role)
            {
                case (int)AccountRole.ADMIN:
                    roleName = AccountRole.ADMIN.ToString();
                    break;

                case (int)AccountRole.STUDENT:
                    roleName = AccountRole.STUDENT.ToString();
                    break;

                case (int)AccountRole.TEACHER:
                    roleName = AccountRole.TEACHER.ToString();
                    break;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var accessToken = new JwtSecurityToken(
                _configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Token:ExpirationInMinutes"])),
                signingCredentials: creds
                );

            var refreshToken = _refreshTokenService.GenerateRefreshToken(email, role);

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return (accessTokenString, refreshToken);
        }

        public FcmToken CheckExistedToken(string email)
            => _tokenRepository.Find(x => x.Email.Equals(email) && x.Active == true);

        public void AddBlacklistedToken(string accessToken, string refreshToken)
        {
            _blackListedToken.Add(accessToken);
            _blackListedToken.Add(refreshToken);
        }
        #endregion

        public bool CreateListAccountExcelFile(string filePath)
        {
            var listData = _mapper.Map<ICollection<AccountReponse>>
                                                  (_repository.GetAll().ToList());

            return ExcelFileCreator.Instance
                .CreateExcelFile(listData, filePath) ;


        }
    }
}

