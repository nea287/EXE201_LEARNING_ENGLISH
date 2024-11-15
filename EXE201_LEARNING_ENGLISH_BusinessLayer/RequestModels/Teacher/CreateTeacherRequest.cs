﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher
{
    public class CreateTeacherRequest
    {
        [Required]
        public string TeacherName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public int? Status { get; set; }
        public string? Level { get; set; }

    }
}
