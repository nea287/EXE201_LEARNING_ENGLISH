using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers.DynamicModelResponse;
using System.Security.Principal;
using Newtonsoft.Json;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;

namespace EXE201_LEARNING_ENGLISH_Admin.Pages.Admin
{
    public class startupModel : PageModel
    {
        public static DynamicModelsResponse<TeacherReponse> result = new DynamicModelsResponse<TeacherReponse>();
        public static DynamicModelsResponse<OrderReponse> lstOrder = new DynamicModelsResponse<OrderReponse>();
        public static DynamicModelsResponse<AccountReponse> lstAccount = new DynamicModelsResponse<AccountReponse>();
        public static DynamicModelsResponse<CourseReponse> lstCourse = new DynamicModelsResponse<CourseReponse>();
        public static ResponseResult<ICollection<CategoryReponse>> lstCate = new ResponseResult<ICollection<CategoryReponse>>();
        public void OnGet()
        {
        }

        [HttpGet("Accounts")]
        public void OnGetAccounts(string data)
        {
            lstAccount = JsonConvert.DeserializeObject<DynamicModelsResponse<AccountReponse>>(data);
        }
        [HttpGet("ListOrder")]
        public void OnGetListOrder(string data)
        {
            lstOrder = JsonConvert.DeserializeObject<DynamicModelsResponse<OrderReponse>>(data);
        }
        [HttpGet("ListTeacher")]
        public void OnGetListTeacher(string data)
        {
            result = JsonConvert.DeserializeObject<DynamicModelsResponse<TeacherReponse>>(data);
            //OnGet();
        }
        [HttpGet("ListCourse")]
        public void OnGetListCourse(string data)
        {
            lstCourse = JsonConvert.DeserializeObject<DynamicModelsResponse<CourseReponse>>(data);
        }
        [HttpGet("CategoryStatistics")]
        public void OnGetCategoryStatistics(string data)
        {
            lstCate = JsonConvert.DeserializeObject<ResponseResult<ICollection<CategoryReponse>>>(data);
        }
    }
}
