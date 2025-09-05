using EXE202_StudentManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<Exe202Context>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"));
});


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
	options.Password.RequiredLength = 3;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;


}).AddEntityFrameworkStores<Exe202Context>()
			.AddDefaultTokenProviders();

var app = builder.Build();


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
				name: "default",
				pattern: "{controller=class}/{action=index}"
);

app.Run();
