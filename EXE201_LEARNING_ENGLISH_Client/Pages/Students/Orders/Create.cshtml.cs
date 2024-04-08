using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Orders
{
    public class CreateModel : PageModel
    {
        public int CourseId { get; set; }
        private readonly IOrderService _orderService;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnGet(int courseId, IOrderService orderService)
        {
            CourseId = courseId;
           // tạo order ở đây
           // nhưng vẫn để ngày approve null

        }

    }
}
