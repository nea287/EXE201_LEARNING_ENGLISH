using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Courses
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IList<StudentCourseViewModel> StudentCourseViewModels { get; set; }
        private int? StudentId;

        [BindProperty]
        public StudentCourseFilter StudentCourseFilter { get; set; }
        [BindProperty]
        public PagingRequest PagingRequest { get; set; } 

        public void OnGet()
        {
            StudentCourseViewModels = new List<StudentCourseViewModel>();
            StudentId = HttpContext.Session.GetInt32("ID");
            StudentCourseFilter ??= new StudentCourseFilter();
            StudentCourseFilter.StudentId = StudentId;
            var studentCourses = _studentService.GetStudentCourses(StudentCourseFilter, PagingRequest);
            foreach (var studentCourse in studentCourses.Results.ToList())
            {
                StudentCourseViewModel studentCourseViewModel = new StudentCourseViewModel
                {
                    StudentCourseId = studentCourse.StudentCourseId,
                    EndDate = studentCourse.EndDate,
                    StartDate = studentCourse.StartDate,
                    Link = studentCourse.Link
                };
                StudentCourseViewModels.Add(studentCourseViewModel);
            }
        }
    }
}
