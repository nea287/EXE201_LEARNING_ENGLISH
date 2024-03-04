using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Account;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Student;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Teacher;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }

        public RegisterModel(IAccountService accountService, ITeacherService teacherService, IStudentService studentService)
        {
            _accountService = accountService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public void OnGet()
        {
            RegisterViewModel = new RegisterViewModel();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreateAccountRequest createAccoutnRequest = new()
            {
                AccessTime = DateTime.Now,
                Address = RegisterViewModel.Address,
                Birthdate = RegisterViewModel.Birthday,
                City = RegisterViewModel.City,
                District = RegisterViewModel.District,
                Email = RegisterViewModel.Email,
                Gender = RegisterViewModel.Gender,
                Password = RegisterViewModel.Password,
                PhoneNumber = RegisterViewModel.PhoneNumber,
                Role = RegisterViewModel.Role,
                Status = 1
            };

            if (_accountService.Register(createAccoutnRequest).result == true)
            {
                HttpContext.Session.SetInt32("ROLE", RegisterViewModel.Role);
                HttpContext.Session.SetString("EMAIL", RegisterViewModel.Email);
                if (RegisterViewModel.Role == 1)
                {
                    CreateStudentRequest createStudentRequest = new()
                    {
                        Email = RegisterViewModel.Email,
                        Status = 1,
                        StudentName = RegisterViewModel.Name
                    };
                    _studentService.CreateStudent(createStudentRequest);
                    HttpContext.Session.SetInt32("ID", (int)_studentService.GetStudent(RegisterViewModel.Email).Value.StudentId);
                    return RedirectToPage("./Students/Courses/Index");
                }
                if (RegisterViewModel.Role == 2)
                {
                    CreateTeacherRequest createTeacherRequest = new()
                    {
                        Email = RegisterViewModel.Email,
                        Status = 1,
                        TeacherName = RegisterViewModel.Name
                    };
                    var p = _teacherService.CreateTeacher(createTeacherRequest);
                    return RedirectToPage("./Teachers/Courses/Index");
                }
            }

            return Page();
        }
    }
}
