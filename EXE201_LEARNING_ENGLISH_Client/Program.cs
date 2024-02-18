using EXE201_LEARNING_ENGLISH_Client.AppStarts;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
builder.Services.ConfigDI();
builder.Services.AddCustomRouting();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<EXE201_LEARNING_ENGLISHContext>(options =>
{
    #region lazyLoad
    options.UseLazyLoadingProxies(); //sử dụng package Microsoft.EntityFrameworkCore.Proxies
    #endregion
    options.UseSqlServer("name=ConnectionStrings:database");
});
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.Run();
