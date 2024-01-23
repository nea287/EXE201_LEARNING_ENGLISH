using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.IServices
{
    public interface ILiveChatService
    {
        public DynamicModelsResponse<LiveChatReponse> GetContents(LiveChatFilter filter, PagingRequest paging);
        public LiveChatReponse GetContent(LiveChatRequest request);
        public bool InsertMessage(LiveChatRequest request);
        public bool DeleteMessage(LiveChatRequest request);
        public Task<bool> SendPrivateMessage(ChatMessageModel message);
        public ICollection<LiveChatReponse> GetMessages();
        public Task<bool> SendMessage(string user, string message);
        //public Task LoginConfirmed(string userId);
        //public Task LoginAnnounce(string userId);
    }
}
