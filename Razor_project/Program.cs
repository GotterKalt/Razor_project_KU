using Microsoft.EntityFrameworkCore;
using Razor_project.Data;
using Razor_project.Models;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        // čia galėtum sugriežtinti slaptažodžių politiką, jei nori
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Razor Pages + autorizacijos taisyklės
builder.Services.AddRazorPages(options =>
{
    // šiuos aplankus leisim tik prisijungusiems
    options.Conventions.AuthorizeFolder("/Projects");
    options.Conventions.AuthorizeFolder("/Tasks");
    options.Conventions.AuthorizeFolder("/Reports");

    // Home/Index, Privacy ir t.t. paliekam visiems
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// SVARBU: pirmiau Authentication, po to Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
