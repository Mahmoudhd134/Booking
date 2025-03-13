using Booking.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<BookingService>(
    client => { client.BaseAddress = new Uri("http://localhost:5104/api/"); });
builder.Services.AddHttpClient<HotelService>(client => { client.BaseAddress = new Uri("http://localhost:5104/api/"); });
builder.Services.AddHttpClient<CustomerServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5104/api/");
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();