using Microsoft.EntityFrameworkCore;
using PaintingManager.Api.Data;
using PaintingManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<AuthService>();

// Controllers
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<ColorService>();
builder.Services.AddScoped<ComponentService>();
builder.Services.AddScoped<FormulaService>();
builder.Services.AddScoped<FormulaLineService>();
builder.Services.AddScoped<MontedService>();

// 🔑 Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 🚀 Ativar CORS antes da autenticação
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
