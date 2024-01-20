using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_LEARNING_ENGLISH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveChatController : ControllerBase
    {
        private readonly ILiveChatService _service;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IConfiguration _configuration;

        public LiveChatController(ILiveChatService service, IHttpClientFactory httpClientFactory/*, IConfiguration configuration*/)
        {
            _service = service;
            _httpClientFactory = httpClientFactory;
            //_configuration = configuration;
        }
        [HttpGet("GetContents")]
        public DynamicModelResponse.DynamicModelsResponse<LiveChatReponse> GetContents([FromQuery] LiveChatFilter filter, [FromQuery] PagingRequest paging)
        {
            return _service.GetContents(filter, paging);
        }

        [HttpGet("GetContent")]
        public LiveChatReponse GetContent([FromQuery] LiveChatRequest request)
        {
            return _service.GetContent(request);
        }

        [HttpPut("InsertContent")]
        public bool InsertContent([FromBody] LiveChatRequest request)
        {
            return _service.InsertMessage(request);
        }

        [HttpDelete("DeleteContent")]
        public bool DeleteContent([FromQuery] LiveChatRequest request)
        {
            return _service.DeleteMessage(request);
        }

        [HttpPost("LiveChat")]
        public Task<bool> LiveChat([FromBody] ChatMessageModel message)
        {
            // Gửi tin nhắn đến API thông qua HttpClient
            //using (var client = _httpClientFactory.CreateClient())
            //{
                //string apiUrl = _configuration.GetValue<string>("ApiSettings:LocalApiUrl") + "LiveChat";
                //var response = await client.PostAsJsonAsync(apiUrl, message);

                return _service.SendPrivateMessage(message);
            //}

        }

        [HttpGet("GetMessagesOfReceivedUser")]
        public ICollection<LiveChatReponse> GetMessagesOfReceivedUser()
        {
            return _service.GetMessages();
        }

        [HttpPost("SendMessageToAll/{user}/{message}")]
        public Task<bool> SendMessageToAll(string user, string message)
        {
            return _service.SendMessage(user, message);
        }
    }
}
