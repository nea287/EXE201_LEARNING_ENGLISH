using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat
{
    public class ChatHub : Hub
    {
        public async Task SendPrivateMessage(string receiver, string user, string message)
        {
            await Clients.User(receiver).SendAsync ("ReceivePrivateMessage", user, message);    
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
    }
}
