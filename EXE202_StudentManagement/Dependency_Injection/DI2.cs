using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;

namespace EXE202_StudentManagement.Dependency_Injection
{

    //TRUONG
    public static class DI2
    {
        public static IServiceCollection AddMyServices2(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();



            return services;
        }
    }
}
