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
        public async Task SendMessage (string receiver, string user, string message)
        {
            await Clients.User(receiver).SendAsync ("ReceivePrivateMessage", user, message);    
        }
    }
}
