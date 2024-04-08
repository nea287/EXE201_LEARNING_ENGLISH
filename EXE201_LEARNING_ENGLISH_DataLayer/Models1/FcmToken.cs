using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class FcmToken
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Fcmtoken1 { get; set; } = null!;
        public bool? Active { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? RefeshToken { get; set; }

        public virtual Account EmailNavigation { get; set; } = null!;
    }
}
