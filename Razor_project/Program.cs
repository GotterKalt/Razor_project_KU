using Razor_project.Data;
using Razor_project.Models;
using Microsoft.EntityFrameworkCore;
using Razor_project.Data;

//builder.Services.AddDbContext<DemoDbContext>();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization(); // vėliau pridėsim autentifikaciją

app.MapRazorPages();

app.Run();
