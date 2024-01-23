using Microsoft.AspNetCore.SignalR;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string receiver, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", receiver, message);
        }
        public async Task SendPrivateMessage(string receiver, string message)
        {
            string userId = Context.ConnectionId;
            await Clients.Client(receiver).SendAsync ("ReceivePrivateMessage", userId, message);    
        }

        public async Task FakeLogin(string userId)
        {
            // Gửi thông điệp xác nhận đăng nhập lại cho client (nếu cần)
            await Clients.Caller.SendAsync("LoginConfirmed", userId);
        }

        public async Task Login(string userId)
        {
            // Đăng ký người dùng khi họ login
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            // Gửi thông báo xác nhận login
            await Clients.Caller.SendAsync("LoginConfirmed", userId);
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("OnConnected", connectionId);
        }
    }
}
