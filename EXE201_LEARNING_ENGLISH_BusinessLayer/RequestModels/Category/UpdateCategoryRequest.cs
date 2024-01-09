using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Category
{
    public class UpdateCategoryRequest
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
