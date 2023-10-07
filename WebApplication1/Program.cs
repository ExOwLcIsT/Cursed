using Cursed.Context;
using Cursed.Hubs;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cursed.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var connectionString = builder.Configuration.GetConnectionString("myDatabase");
builder.Services.AddDbContext<GameContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<GameContext>()
        .AddDefaultTokenProviders();
builder.Services.AddTransient<SkinRepository>();
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

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.MapHub<GameHub>("/gameHub");
app.Run();
