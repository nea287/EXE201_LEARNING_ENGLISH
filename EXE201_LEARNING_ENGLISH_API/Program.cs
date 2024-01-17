using EXE201_LEARNING_ENGLISH_API.AppStarts;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Services.LiveChat;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    #region JWT
    //Khai bao bearer token trong swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        BearerFormat = "JWT",
        Description = "Enter your token",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        #region quan trọng
        //Nếu không có cái này thì không thể xác minh token
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
        #endregion
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    //Them xac thuc cho Swagger
    //var securityRequirement = new OpenApiSecurityRequirement
    //{
    //    {securityScheme, new []{"Bearer"} }
    //};

    //c.AddSecurityRequirement(securityRequirement);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                securityScheme,
                new string[] {"Bearer"} //new string[] {} Như nhau
            }
        });
    #endregion
});

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

#region Identity
// add Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<EXE201_LEARNING_ENGLISHContext>()
//    .AddDefaultTokenProviders();
#endregion

#region Cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAnyOrigins", options => 
        options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion

#region JWT

var tokenConfig = builder.Configuration.GetSection("Token");
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // clear default behaviour không ảnh hưởng đến jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig["SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        //ClockSkew = TimeSpan.Zero // remove delay of token when expire
    };
});

//authorization
builder.Services.AddAuthorization(options =>
{
    //options.AddPolicy("RequireAdminRole", policy =>
    //{
    //    policy.RequireRole("Admin");
    //});
    options.AddPolicy("Bearer", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

#endregion

var app = builder.Build();

app.UseRouting(); //trước authen

app.UseAuthentication(); //trước useEndpoints

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Thêm Middleware để xử lý Bearer Token trong Swagger UI
        //OAuthUseBasicAuthenticationWithAccessCodeFlow
        //c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        //c.OAuthClientId("swagger");
        //c.OAuthClientSecret("swagger-secret");
    });
}





app.UseHttpsRedirection();


//app.MapControllers();



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
    endpoints.MapControllers();
});

app.UseCors("AllowAnyOrigins");

app.Run();
