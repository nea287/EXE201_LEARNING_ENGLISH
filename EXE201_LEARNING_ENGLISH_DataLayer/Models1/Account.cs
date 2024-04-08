using System;
using System.Collections.Generic;

namespace EXE201_LEARNING_ENGLISH_DataLayer.Models1
{
    public partial class Account
    {
        public Account()
        {
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public int? Role { get; set; }
        public int? Status { get; set; }
        public DateTime? AccessTime { get; set; }
        public string? FaceId { get; set; }
        public string? TouchId { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? Avatar { get; set; }

        public virtual FcmToken? FcmToken { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
