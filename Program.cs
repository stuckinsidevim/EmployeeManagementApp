using EmployeeManagementApp.Data;
using EmployeeManagementApp.Data.Repositories;
using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Profiles;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(InMemoryDataContext.Instance);
builder.Services.AddScoped<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<ILeaveRepository, InMemoryLeaveRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LeaveService>();
builder.Services.AddScoped<IAuthService<Employee>, UserService>();
builder.Services.AddAutoMapper(typeof(EmployeeManagementProfile));

builder.Services.AddControllers();
// NOTE: either remove jsonignore or do the below
// builder.Services.AddControllers().AddJsonOptions(options =>
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
// );
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
builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
