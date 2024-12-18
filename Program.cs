using Microsoft.EntityFrameworkCore;
using CustomerInvoiceManagementSystem.Entities;
using CustomerInvoiceManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

//Register CustomerService (scoped service)
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<InvoiceService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//connecting to the database

string connstr = builder.Configuration.GetConnectionString("CustomerInvoiceManagementDB");
builder.Services.AddDbContext<CustomerInvoiceManagementDbContext>(options => options.UseSqlServer(connstr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
