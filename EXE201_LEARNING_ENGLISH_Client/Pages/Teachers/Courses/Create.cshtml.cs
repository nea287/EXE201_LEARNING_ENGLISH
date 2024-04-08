using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.ReponseModels.Heplers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Teachers.Courses
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public int SelectedCategoryId { get; set; }
        

        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CreateModel(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public CreateCourseRequest CreateCourseRequest { get; set; }
        public IList<CategoryReponse> Categories { get; private set; }

        public void OnGet() => LoadData();

        public IActionResult OnPost()
        {          
            CreateCourseRequest.CategoryId = SelectedCategoryId;
            CreateCourseRequest.TeacherId = HttpContext.Session.GetInt32("ID");
            CreateCourseRequest.Status = 1;
            _courseService.CreateCourse(CreateCourseRequest);
            return RedirectToPage("/Teachers/Courses/Index");
        }

        public void LoadData()
        {
            CategoryFilter filter = new();
            PagingRequest request = new();
            Categories = _categoryService.GetCategorys(filter, request).Results;
        }
    }
}
