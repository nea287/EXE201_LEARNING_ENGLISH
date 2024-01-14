using AutoMapper;
using DlibDotNet;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ICollection<LiveChatReponse> GetContents()
        {
            ICollection<LiveChatReponse> result = new List<LiveChatReponse>();
            try
            {
                result = _mapper.Map<ICollection<LiveChatReponse>>(
                   _context.Users.Find(_ => true).ToList());
                //_mapper.Map<ICollection<LiveChatReponse>>(_context.Users.Find(new BsonDocument()).ToList());


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
            
            await _hubContext.Clients.User(message.Receiver).SendAsync("ReceiveMessage", message.Sender, message.Message);

            LiveChatRequest data = _mapper.Map<LiveChatRequest>(message);
            data.Timestamp = DateTime.Now;
            return InsertMessage(data);
        }
    }
}
