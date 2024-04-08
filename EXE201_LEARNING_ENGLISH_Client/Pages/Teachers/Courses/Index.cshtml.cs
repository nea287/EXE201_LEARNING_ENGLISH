using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Teachers.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public string CurrentFilter { get; set; }
        public PaginatedList<CourseViewModel> CourseList { get; set; }

        public void OnGet(string? SearchName, int? pageIndex)
        {
            int teacherId = (int) HttpContext.Session.GetInt32("ID");
            CourseList = new();
            foreach (var courses in _courseService.GetCoursesByTeacherId(teacherId))
            {
                CourseList.Add(new CourseViewModel
                {
                    Description = courses.Description,
                    Duration = courses.Duration,
                    CourseName = courses.CourseName,
                    NumberOfLesson = courses.NumberOfLesson,
                    UnitPrice = courses.UnitPrice,
                    CourseId = courses.CourseId,
                    Image = Convert.ToBase64String(courses.Image),
                    TeacherName = courses.Teacher.TeacherName
                });
            }
            IQueryable<CourseViewModel> courseIQs = CourseList.AsQueryable();
            if (!String.IsNullOrEmpty(SearchName))
                courseIQs = courseIQs.Where(s => s.CourseName.Contains(SearchName));
            CourseList = PaginatedList<CourseViewModel>.Create(
                courseIQs.AsNoTracking(), pageIndex ?? 1, 3);
        }
    }
}
