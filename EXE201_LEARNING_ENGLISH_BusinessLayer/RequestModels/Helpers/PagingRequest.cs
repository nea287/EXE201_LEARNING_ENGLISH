using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers
{
    public class PagingRequest
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string KeySearch { get; set; } = "";
        public string SearchBy { get; set; } = "";
        public SortOrder SortType { get; set; } = SortOrder.Descending;
        public string CollName { get; set; } = "Id";
    }
}
