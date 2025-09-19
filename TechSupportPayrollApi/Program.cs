using TechSupportPayrollApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using TechSupportPayrollApi.Services;
using TechSupportPayrollApi.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Добавила контекст БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=techsupport.db"));

builder.Services.AddScoped<SalaryCalculationService>(); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TechSupport Payroll API",
        Version = "v1",
        Description = "API для расчета зарплаты операторов техподдержки"
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:3000") // URL фронтенда
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();