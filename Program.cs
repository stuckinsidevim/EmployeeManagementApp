using EmployeeManagementApp.Data;
using EmployeeManagementApp.Data.Repositories;
using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(InMemoryDataContext.Instance);
builder.Services.AddScoped<IEmployeeRepository, InMemoryEmployeeRepository>();
builder.Services.AddScoped<ILeaveRepository, InMemoryLeaveRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<LeaveService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
