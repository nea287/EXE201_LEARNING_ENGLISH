using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student
{
    public class CreateStudentRequest
    {
        [Required]
        public string StudentName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
    }
}
