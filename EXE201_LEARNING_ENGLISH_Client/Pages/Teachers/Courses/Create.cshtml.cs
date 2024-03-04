using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Course;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Teachers.Courses
{
    public class CreateModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public CreateCourseRequest CreateCourseRequest { get; set; }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            CreateCourseRequest.TeacherId = HttpContext.Session.GetInt32("ID");
            _courseService.CreateCourse(CreateCourseRequest);
            return RedirectToPage("/Index");
        }
    }
}
