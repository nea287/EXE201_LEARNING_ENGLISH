using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public IList<CourseViewModel> CourseList;

        public void OnGet()
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
    }
}
