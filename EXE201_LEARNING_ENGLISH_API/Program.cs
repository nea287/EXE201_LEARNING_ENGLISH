using EXE201_LEARNING_ENGLISH_API.AppStarts;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
builder.Services.ConfigDI();
builder.Services.AddCustomRouting();
builder.Services.AddHttpClient();

#region database
builder.Services.AddDbContext<EXE201_LEARNING_ENGLISHContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:database"));
#endregion 

#region Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheUrl"];
});
#endregion 

#region SignalR
builder.Services.AddSignalR();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.Run();
