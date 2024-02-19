using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EXE201_LEARNING_ENGLISH_Client.Pages
{
    public class LoginViewModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public LoginModel(IAccountService accountService,
                            IStudentService studentService,
                            ITeacherService teacherService)
        {
            _accountService = accountService;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = _accountService.Login(LoginViewModel.Email, LoginViewModel.Password);
            if (account != null)
            {
                switch (account.Value.Role)
                {
                    case 1:
                        HttpContext.Session.SetInt32("ID", (int) _studentService.GetStudent(account.Value.Email).Value.StudentId);
                        return RedirectToPage("./Students/Course/Index");
                }
            }
            return Page();
        }
    }
}
