using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher
{
    public class UpdateTeacherRequest
    {
        public string? TeacherName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? Level { get; set; }
    }
}
