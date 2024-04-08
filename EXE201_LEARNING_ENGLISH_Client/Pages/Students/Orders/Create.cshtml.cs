using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Orders
{
    public class CreateModel : PageModel
    {
        public int CourseId { get; set; }
        private readonly IOrderService _orderService;
        public string Message;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnGet(int courseId)
        {
            CourseId = courseId;
            int id = (int) HttpContext.Session.GetInt32("ID");
            CreateOrderRequest createOrderRequest = new()
            {
                CheckInDate = DateTime.Now,
                StudentId = id,
                Status = "PENING"
            };
            Message = $"Nội dung giao dịch: ST{id}CO{courseId}";
            _orderService.CreateOrder(createOrderRequest);
        }
    }
}
