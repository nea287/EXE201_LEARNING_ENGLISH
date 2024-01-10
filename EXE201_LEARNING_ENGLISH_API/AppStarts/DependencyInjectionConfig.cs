using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services;
using EXE201_LEARNING_ENGLISH_DAO.DAO;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using EXE201_LEARNING_ENGLISH_Repository.Repository;

namespace EXE201_LEARNING_ENGLISH_API.AppStarts
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, IOrderDetailService>();
        }
    }
}
