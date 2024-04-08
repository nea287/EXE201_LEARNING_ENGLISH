using EXE201_LEARNING_ENGLISH_BusinessLayer.FilterModels;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.Order;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Admins.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly IOrderService _orderSerivce;
        private readonly ICourseService _courseService;
        [BindProperty]
        public PaginatedList<OrderViewModel> OrderViewModels { get; set; }

        public IndexModel(IOrderService orderSerivce, ICourseService courseService)
        {
            _orderSerivce = orderSerivce;
            _courseService = courseService;
        }

        public void OnGet() => LoadData();

        public IActionResult OnPostUpdate(int id)
        {
            var order = _orderSerivce.GetOrder(id).Value;
            UpdateOrderRequest request = new()
            {
                ApproveDate = DateTime.Now,
                CheckInDate = order.CheckInDate,
                Discount = order.Discount,
                FinalAmount = order.FinalAmount,
                OrderDetails = null,
                Quantity = 1,
                StudentId = order.StudentId,
                TotalAmount = order.TotalAmount,
                VouncherId = order.VouncherId,
            };
            _orderSerivce.UpdateOrder(request, id, Int32.Parse(order.CourseId));
            return RedirectToPage("/Admins/Orders/Index");
        }

        private void LoadData()
        {
            OrderViewModels = new();
            OrderFilter orderFilter = new();
            PagingRequest pagingRequest = new();
            foreach (var order in _orderSerivce.GetOrders(orderFilter, pagingRequest).Results)
            {
                var momoNumber = _courseService.GetCourse(Int32.Parse(order.CourseId)).Value.MomoNumber;
                OrderViewModels.Add(new OrderViewModel
                {
                    CheckInDate = order.CheckInDate,
                    FinalAmount = order.FinalAmount,
                    Discount = order.Discount,
                    Status = order.Status,
                    OrderId = (int)order.OrderId,
                    TotalAmount = order.TotalAmount,
                    CourseId = order.CourseId,
                    MomoNumber = momoNumber,
                    ApproveDate = order.ApproveDate
                });
            }
        }
    }
}
