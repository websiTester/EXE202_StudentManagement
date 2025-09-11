using EXE202_StudentManagement.Dependency_Injection;
using EXE202_StudentManagement.Models;
using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddMyServices1();
builder.Services.AddMyServices2();
builder.Services.AddMyServices3();
builder.Services.AddMyServices4();



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


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
