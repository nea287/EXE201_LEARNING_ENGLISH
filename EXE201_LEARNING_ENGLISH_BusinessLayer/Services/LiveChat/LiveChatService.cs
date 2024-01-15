using AutoMapper;
using AutoMapper.QueryableExtensions;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Common;
using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using System.Text;
using XAct;
namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat
{
    public class LiveChatService : ILiveChatService
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IDistributedCache _cache;

        public LiveChatService(MongoDBContext context, IMapper mapper, IHubContext<ChatHub> hubContext, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            _cache = cache;
        }
        public bool DeleteMessage(LiveChatRequest request)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter
                    .Where(x => x.SenderId.Equals(request.SenderId, StringComparison.Ordinal)
                        && x.ReceiverId.Equals(request.ReceiverId)
                        && x.Timestamp == request.Timestamp
                        && x.Status == 1);

                UpdateDefinition<User> update = Builders<User>.Update
                    .Set(u => u.Status, 0);

                _context.Users.FindOneAndUpdate(filter, update,
                     new FindOneAndUpdateOptions<User> { ReturnDocument = ReturnDocument.After });

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                lock (_context) ;
            }

            return true;
        }

        public LiveChatReponse GetContent(LiveChatRequest request)
        {
            LiveChatReponse result = new LiveChatReponse();
            try
            {
                var data = _context.Users.Find(x => x.Timestamp == request.Timestamp
                    && x.Content.Equals(request.Content)
                    && x.SenderId.Equals(request.SenderId)
                    && x.ReceiverId.Equals(request.ReceiverId)
                    && x.Status == 1).FirstOrDefault();

                result = _mapper.Map<LiveChatReponse>(data);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                lock (_context) ;
            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<LiveChatReponse> GetContents(LiveChatFilter filter, PagingRequest paging)
        {
            (int, IQueryable<LiveChatReponse>) result;

            #region unassigned
            result.Item1 = 0;
            result.Item2 = new List<LiveChatReponse>().AsQueryable();
            #endregion

            try
            {
                   
                IQueryable<User> data = _context.Users.Find(_ => true).ToList().AsQueryable();

                result = data.ProjectTo<LiveChatReponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(_mapper.Map<LiveChatReponse>(filter))
                    .PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                //_mapper.Map<ICollection<LiveChatReponse>>(_context.Users.Find(new BsonDocument()).ToList());


            }
            catch (Exception ex)
            {

            }
            finally
            {
                lock (_context) ;
            }

            return new DynamicModelResponse.DynamicModelsResponse<LiveChatReponse>()
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

        public bool InsertMessage(LiveChatRequest request)
        {
            try
            {
                User data = _mapper.Map<User>(request);
                data.Status = 1;

                _context.Users.InsertOne(data);

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                lock (_context) ;
            }

            return true;
        }

        public ICollection<LiveChatReponse> GetMessages()
        {
            ICollection<LiveChatReponse> result = new List<LiveChatReponse>();
            try
            {
                string receivedUser = Encoding.UTF8.GetString(_cache.Get("-email"));

                result = _mapper.Map<ICollection<LiveChatReponse>>(
                   _context.Users.Find(x => x.ReceiverId.Equals(receivedUser)).ToList());


            }
            catch (Exception ex)
            {

            }
            finally
            {
                lock (_context) ;
            }

            return result;
        }

        public async Task<bool> SendMessage(ChatMessageModel message)
        {
            
            await _hubContext.Clients.User(message.Receiver).SendAsync("ReceivePrivateMessage", message.Sender, message.Message);

            LiveChatRequest data = _mapper.Map<LiveChatRequest>(message);
            data.Timestamp = DateTime.UtcNow;
            return InsertMessage(data);
        }
    }
}
