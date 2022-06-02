using Lab_1;
using Lab_1.Models;
using Lab_1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using Lab_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ���������, ����� �� �������������� �������� ��� ��������� ������
            ValidateIssuer = true,
            // ������, �������������� ��������
            ValidIssuer = JwtConfigurations.Issuer,
            // ����� �� �������������� ����������� ������
            ValidateAudience = true,
            // ��������� ����������� ������
            ValidAudience = JwtConfigurations.Audience,
            // ����� �� �������������� ����� �������������
            ValidateLifetime = true,
            // ��������� ����� ������������
            IssuerSigningKey = JwtConfigurations.GetSymmetricSecurityKey(),
            // ��������� ����� ������������
            ValidateIssuerSigningKey = true,
        };
    });


// Services: 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IInvestmentBagService, InvestmentBagService>();


// DB:
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connection));


var app = builder.Build();

// JWT
app.UseAuthentication();
app.UseAuthorization();

//DB init and update:
/*using var serviceScope = app.Services.CreateScope();
var dbContext = serviceScope.ServiceProvider.GetService<Context>();
dbContext?.Database.Migrate(); // Migration*/

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
