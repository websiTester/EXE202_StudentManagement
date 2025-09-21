using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Dependency_Injection
{

    //TAN
    public static class DI3
    {
        public static IServiceCollection AddMyServices3(this IServiceCollection services)
        {
            services.AddScoped<IClassRepository2, ClassRepository2>();
            services.AddScoped<IClassService2, ClassService2>();


            return services;
        }
    }
}
