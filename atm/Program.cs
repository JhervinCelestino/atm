var builder = WebApplication.CreateBuilder(args);

// Enable MVC and Session
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

app.UseRouting();
app.UseSession(); // ✅ Activate session

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=ATM}/{action=Welcome}/{id?}"); // ✅ Default page = Welcome
});

app.Run();