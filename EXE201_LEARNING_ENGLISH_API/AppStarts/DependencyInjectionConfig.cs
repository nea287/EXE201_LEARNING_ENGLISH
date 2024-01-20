using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat;
using EXE201_LEARNING_ENGLISH_DAO.DAO;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using EXE201_LEARNING_ENGLISH_Repository.Repository;
using XAct;

namespace EXE201_LEARNING_ENGLISH_API.AppStarts
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IVouncherService, VouncherService>();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IOrderService, OrderService>();

            #region MongoDb
            services.AddSingleton<MongoDBContext>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                //var connectionString = configuration.GetConnectionString("MongoDBSettings");
                var connectionString = configuration.GetSection("MongoDBSettings:MongoDBConnection").Value;
                var databaseName = configuration.GetSection("MongoDBSettings:DatabaseName").Value;
                return new MongoDBContext(connectionString, databaseName);
            });
            #endregion

            #region LiveChat
            services.AddScoped<ILiveChatService, LiveChatService>();
            #endregion

            #region Token
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            #endregion
        }
    }
}
