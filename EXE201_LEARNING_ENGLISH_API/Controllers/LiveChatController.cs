using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
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
        public ICollection<LiveChatReponse> GetContents()
        {
            return _service.GetContents();
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
        public async Task<Task<bool>> LiveChat([FromBody] ChatMessageModel message)
        {
            // Gửi tin nhắn đến API thông qua HttpClient
            //using (var client = _httpClientFactory.CreateClient())
            //{
                //string apiUrl = _configuration.GetValue<string>("ApiSettings:LocalApiUrl") + "LiveChat";
                //var response = await client.PostAsJsonAsync(apiUrl, message);

                return _service.SendMessage(message);
            //}

        }

        [HttpGet("GetMessagesOfReceivedUser")]
        public ICollection<LiveChatReponse> GetMessagesOfReceivedUser()
        {
            return _service.GetMessages();
        }
    }
}
