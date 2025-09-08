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
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IGroupRepository, GroupRepostiory>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupTaskRepository, GroupTaskRepository>();
builder.Services.AddScoped<IGroupTaskService, GroupTaskService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IStudentClassRepository, StudentClassRepository>();
builder.Services.AddScoped<IStudentClassService, StudentClassService>();
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
