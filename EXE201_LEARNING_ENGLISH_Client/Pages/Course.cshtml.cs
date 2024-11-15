using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class CourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public string CurrentFilter { get; set; }
        public PaginatedList<CourseViewModel> CourseList { get; set; }

        public void OnGet(string? SearchName, int? pageIndex)
        {
            PagingRequest pagingRequest = new();
            CourseFilter courseFilter = new();
            CourseList = new();
            foreach (var courses in _courseService.GetCourses(courseFilter, pagingRequest).Results)
            {
                CourseList.Add(new CourseViewModel
                {
                    Description = courses.Description,
                    Duration = courses.Duration,
                    CourseName = courses.CourseName,
                    NumberOfLesson = courses.NumberOfLesson,
                    UnitPrice = courses.UnitPrice,
                    CourseId = (int)courses.CourseId,
                    Image = Convert.ToBase64String(courses.Image),
                    TeacherName = courses.TeacherName
                });
            }
            IQueryable<CourseViewModel> courseIQs = CourseList.AsQueryable();
            if (!String.IsNullOrEmpty(SearchName))
                courseIQs = courseIQs.Where(s => s.CourseName.Contains(SearchName));
            CourseList = PaginatedList<CourseViewModel>.Create(
                courseIQs.AsNoTracking(), pageIndex ?? 1, 9);
        }
    }
}

//        private readonly ICourseService _courseService;

//        public CourseModel(ICourseService courseService)
//        {
//            _courseService = courseService;
//        }

//        public CourseViewModel CourseViewModel;

//        public int CourseId { get; private set; }
//        public IList<CourseViewModel> CourseList;

//        public IActionResult OnGet()
//        {
//            string courseIdString = Request.Query["id"];
//            if (courseIdString != null)
//            {
//                if (int.TryParse(courseIdString, out int courseId))
//                {
//                    CourseId = courseId;
//                }
//                var course = _courseService.GetCourse(CourseId);
//                CourseViewModel = new CourseViewModel()
//                {
//                    CourseId = (int) course.Value.CourseId,
//                    Description = course.Value.Description,
//                    CourseName = course.Value.CourseName,
//                    TeacherName = course.Value.TeacherName,
//                    UnitPrice = course.Value.UnitPrice,
//                    Duration = course.Value.Duration,
//                    NumberOfLesson = course.Value.NumberOfLesson
//                };
//            }
//            else
//            {
//                CourseList = new List<CourseViewModel>();
//                PagingRequest pagingRequest = new PagingRequest();
//                CourseFilter courseFilter = new CourseFilter();
//                var courseList = _courseService.GetCourses(courseFilter, pagingRequest);
//                foreach (var course in courseList.Results)
//                {
//                    CourseViewModel courseViewModel = new CourseViewModel();
//                    courseViewModel.CourseId = (int) course.CourseId;
//                    courseViewModel.CourseName = course.CourseName;
//                    courseViewModel.Description = course.Description;
//                    courseViewModel.UnitPrice = course.UnitPrice;
//                    courseViewModel.TeacherName = course.TeacherName;
//                    CourseList.Add(courseViewModel);
//                }
//            }

//            return Page();
//        }

//        public IActionResult OnPost(int courseId)
//        {
//            //if (HttpContext.Session.GetInt32("ID") == null)
//            //{
//            //    return RedirectToPage("/Login");
//            //}
//            //if (HttpContext.Session.GetInt32("ROLE") == 1)
//            //{
//            //    return RedirectToPage($"/Students/Orders/Create", courseId);
//            //}
//            //return Page();
//            return RedirectToPage("/Students/Orders/Create", new { courseId });
//        }
//    }
//}
