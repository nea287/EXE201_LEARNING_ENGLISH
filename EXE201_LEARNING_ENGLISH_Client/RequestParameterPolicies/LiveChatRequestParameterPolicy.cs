using Microsoft.AspNetCore.Mvc.Abstractions;

namespace EXE201_LEARNING_ENGLISH_API.RequestParameterPolicies
{
    public class LiveChatRequestParameterPolicy : IParameterPolicy
    {
        public bool HasMatch(RouteContext routeContext, ActionDescriptor action)
        {
            // Xác định xem có phải là một sự kết hợp hợp lệ hay không
            // Thực hiện kiểm tra logic tại đây và trả về true hoặc false tùy thuộc vào điều kiện

            // Ví dụ: Kiểm tra xem giá trị của tham số có đúng định dạng LiveChatRequest hay không
            //return IsValidLiveChatRequest(routeContext.RouteData.Values["LiveChatRequest"]?.ToString());
            return true;
        }

        public IReadOnlyList<ParameterDescriptor> GetParameters()
        {
            // Trả về danh sách các tham số mà policy áp dụng
            // Thường chỉ có một tham số khi sử dụng constraint cho một tham số đơn
            return new List<ParameterDescriptor>
        {
            new ParameterDescriptor { Name = "LiveChatRequest" }
        };
        }
    }
}