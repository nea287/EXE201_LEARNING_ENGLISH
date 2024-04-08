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
        private readonly ICourseService _courseService;
        public string Message;

        public CreateModel(IOrderService orderService, ICourseService courseService)
        {
            _orderService = orderService;
            _courseService = courseService;
        }

        public void OnGet(int courseId)
        {
            CourseId = courseId;
            int id = (int) HttpContext.Session.GetInt32("ID");
            var course = _courseService.GetCourse(CourseId).Value;
            CreateOrderRequest createOrderRequest = new()
            {
                CheckInDate = DateTime.Now,
                StudentId = id,
                Status = "PENING",
                TotalAmount = course.UnitPrice,
                CourseId = courseId,
                FinalAmount = course.UnitPrice - (course.UnitPrice * 20 / 100)
            };
            Message = $"Nội dung giao dịch: ST{id}CO{courseId}";
            _orderService.CreateOrder(createOrderRequest);
        }
    }
}
