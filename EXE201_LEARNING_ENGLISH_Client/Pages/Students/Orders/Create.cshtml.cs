using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Orders
{
    public class CreateModel : PageModel
    {
        public int CourseId { get; set; }

        public void OnGet(int courseId)
        {
            CourseId = courseId;
        }

    }
}
