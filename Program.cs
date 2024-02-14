using Microsoft.EntityFrameworkCore;
using Payment_Backend_Service.Data;
using Payment_Backend_Service.Repository;
using Payment_Backend_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentManagementSystemConnectionString")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


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
