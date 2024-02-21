using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
#region database
builder.Services.AddDbContext<EXE201_LEARNING_ENGLISHContext>(options =>
{
    #region lazyLoad
    options.UseLazyLoadingProxies(); //sử dụng package Microsoft.EntityFrameworkCore.Proxies
    #endregion
    options.UseSqlServer("name=ConnectionStrings:database");
});
#endregion 


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

app.Run();
