﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student
{
    public class UpdateStudentRequest
    {
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
    }
}
