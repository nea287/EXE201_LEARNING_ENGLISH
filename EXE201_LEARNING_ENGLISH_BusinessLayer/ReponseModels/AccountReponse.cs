using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels
{
    public class AccountReponse
    {
        public AccountReponse()
        {
            Students = new HashSet<StudentReponse>();
            Teachers = new HashSet<TeacherReponse>();
        }

        public string? Email { get; set; }
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

        public ICollection<StudentReponse>? Students { get; set; }
        public ICollection<TeacherReponse>? Teachers { get; set; }
    }
}
