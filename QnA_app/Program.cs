using Microsoft.EntityFrameworkCore;
using QnA_app.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<QnA_DBContext>(item=>item.UseSqlServer(config.GetConnectionString(("QnA_DB_Con"))));
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login_Signup}/{id?}");

app.Run();
