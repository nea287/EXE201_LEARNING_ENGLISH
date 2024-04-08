using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Teachers.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IList<CourseViewModel> CourseList { get; set; }

        public void OnGet()
        {
            int teacherId = (int) HttpContext.Session.GetInt32("ID");
            CourseList = new List<CourseViewModel>();
            foreach (var courses in _courseService.GetCoursesByTeacherId(teacherId))
            {
                CourseList.Add(new CourseViewModel
                {
                    Description = courses.Description,
                    Duration = courses.Duration,
                    CourseName = courses.CourseName,
                    NumberOfLesson = courses.NumberOfLesson,
                    UnitPrice = courses.UnitPrice,
                    CourseId = courses.CourseId
                });
            }
        }
    }
}
