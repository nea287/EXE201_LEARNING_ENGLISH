using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ILiveChatService
    {
        public ICollection<LiveChatReponse> GetContents();
        public LiveChatReponse GetContent(LiveChatRequest request);
        public bool InsertMessage(LiveChatRequest request);
        public bool DeleteMessage(LiveChatRequest request);
        public Task<bool> SendMessage(ChatMessageModel message);
        public ICollection<LiveChatReponse> GetMessages();
    }
}
