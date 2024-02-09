using EmployeeManagementApp.Data;
using EmployeeManagementApp.Data.Repositories;
using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(InMemoryDataContext.Instance);
builder.Services.AddScoped<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<ILeaveRepository, InMemoryLeaveRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LeaveService>();

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "EmployeeManagementApp.Auth";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var response = new { message = "Authentication required." };
                return context.Response.WriteAsJsonAsync(response);
            }
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = context =>
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                var response = new { message = "Access denied. You do not have permission to access this resource." };
                return context.Response.WriteAsJsonAsync(response);
            }
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        }
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
