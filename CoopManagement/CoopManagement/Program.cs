using CoopManagementApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext to use SQL Server and the connection string
builder.Services.AddDbContext<CoopManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Optionally add caching
// builder.Services.AddDistributedMemoryCache(); // For in-memory caching
// builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(30); }); // If you use session-based caching

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();  // Optional: Recommended for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Ensure static files are served

app.UseRouting();

// Authorization middleware (you can configure authentication if needed)
app.UseAuthorization();

// Set up default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();