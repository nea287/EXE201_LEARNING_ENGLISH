using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Course
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

        public void OnGet()
        {
            StudentCourseViewModels = new List<StudentCourseViewModel>();
            StudentId = HttpContext.Session.GetInt32("ID");
            StudentCourseFilter filter = new StudentCourseFilter();
            PagingRequest paging = new PagingRequest();
            var studentCourses = _studentService.GetStudentCoursesByStudentId(filter, paging, StudentId);
            foreach (var studentCourse in studentCourses)
            {
                StudentCourseViewModel studentCourseViewModel = new StudentCourseViewModel
                {
                    StudentCourseId = studentCourse.StudentCourseId,
                    CourseName = studentCourse.Course.CourseName,
                    EndDate = studentCourse.EndDate,
                    StartDate = studentCourse.StartDate,
                    Link = studentCourse.Link
                };
                StudentCourseViewModels.Add(studentCourseViewModel);
            }
        }
    }
}
