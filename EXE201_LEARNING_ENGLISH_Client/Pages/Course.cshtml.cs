using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class CourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public CourseViewModel CourseViewModel;

        public int CourseId { get; private set; }
        public IList<CourseViewModel> CourseList;

        public IActionResult OnGet()
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
                    Id = course.Value.CourseId,
                    Description = course.Value.Description,
                    Name = course.Value.CourseName,
                    TeacherName = course.Value.TeacherName,
                    UnitPrice = course.Value.UnitPrice,
                    Duration = course.Value.Duration,
                    NumberOfLesson = course.Value.NumberOfLesson
                };
            }
            else
            {
                CourseList = new List<CourseViewModel>();
                PagingRequest pagingRequest = new PagingRequest();
                CourseFilter courseFilter = new CourseFilter();
                var courseList = _courseService.GetCourses(courseFilter, pagingRequest);
                foreach (var course in courseList.Results)
                {
                    CourseViewModel courseViewModel = new CourseViewModel();
                    courseViewModel.Id = course.CourseId;
                    courseViewModel.Name = course.CourseName;
                    courseViewModel.Description = course.Description;
                    courseViewModel.UnitPrice = course.UnitPrice;
                    courseViewModel.TeacherName = course.TeacherName;
                    CourseList.Add(courseViewModel);
                }
            }

            return Page();
        }
    }
}
