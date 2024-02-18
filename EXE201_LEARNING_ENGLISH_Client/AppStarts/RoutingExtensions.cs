using EXE201_LEARNING_ENGLISH_API.RequestParameterPolicies;
using EXE201_LEARNING_ENGLISH_BusinessLayer.RequestModels.LiveChat;

namespace EXE201_LEARNING_ENGLISH_Client.AppStarts
{
    public static class RoutingExtensions
    {
        public static void AddCustomRouting(this IServiceCollection services)
        {
            // Thực hiện đăng ký cấu hình định tuyến tùy chọn tại đây
            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("LiveChatRequest", typeof(LiveChatRequestParameterPolicy));
            });

            // Các đăng ký định tuyến khác nếu cần
            // ...
        }
    }
}
