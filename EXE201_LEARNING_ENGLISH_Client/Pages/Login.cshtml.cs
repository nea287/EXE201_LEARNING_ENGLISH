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

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
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
                HttpContext.Session.SetInt32("ROLE", (int) account.Value.Role);
                switch (account.Value.Role)
                {
                    case 1:
                        return RedirectToPage("./Courses/Index");
                    case 2:
                        return RedirectToPage("./Courses/Index");
                }
            }
            return Page();
        }
    }
}
