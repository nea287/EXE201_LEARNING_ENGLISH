using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class CourseDetailModel : PageModel
    {
        public int CourseId { get; private set; }
        public CourseViewModel CourseViewModel { get; set; }
        private ICourseService _courseService { get; set; }
        public CourseDetailModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet()
        {
            string courseIdString = Request.Query["id"];
            if (courseIdString != null)
            {
                if (int.TryParse(courseIdString, out int courseId))
                {
                    CourseId = courseId;
                }
                var course = _courseService.GetCourse(CourseId);
                CourseViewModel = new CourseViewModel()
                {
                    CourseId = (int)course.Value.CourseId,
                    Description = course.Value.Description,
                    CourseName = course.Value.CourseName,
                    TeacherName = course.Value.TeacherName,
                    UnitPrice = course.Value.UnitPrice,
                    Duration = course.Value.Duration,
                    NumberOfLesson = course.Value.NumberOfLesson,
                    Image = Convert.ToBase64String(course.Value.Image)
                };
            }
        }

        public IActionResult OnPost(int courseId)
        {
            if (HttpContext.Session.GetInt32("ID") == null)
            {
                return RedirectToPage("/Login");
            }
            if (HttpContext.Session.GetInt32("ROLE") == 1)
            {
                return RedirectToPage($"/Students/Orders/Create", new { courseId = courseId });
            }
            return Page();
        }
    }
}
