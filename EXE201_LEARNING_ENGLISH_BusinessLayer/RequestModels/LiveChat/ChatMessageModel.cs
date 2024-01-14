using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat
{
    public class ChatMessageModel
    {
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Receiver { get; set; }
    }
}
