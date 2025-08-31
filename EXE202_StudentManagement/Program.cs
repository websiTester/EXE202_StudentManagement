using EXE202_StudentManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
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

	options.SignIn.RequireConfirmedEmail = true;

}).AddEntityFrameworkStores<Exe202Context>()
			.AddDefaultTokenProviders();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action}"
);

app.Run();
