using Booking.Application;
using Booking.DataAccess;
using Booking.DataAccess.Data;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccess(builder.Configuration.GetSection("DataAccess"));
builder.Services.AddApplication();


builder.Services.AddCors(
    o => o.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

try
{
    var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<BookingDbContext>();
    await context.Database.MigrateAsync();
}
catch
{
    // ignored
}

app.Run();