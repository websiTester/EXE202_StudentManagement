using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;

namespace EXE202_StudentManagement.Dependency_Injection
{

    //VU
    public static class DI4
    {


        public static IServiceCollection AddMyServices4(this IServiceCollection services)
        {
            services.AddScoped<ITeacherMarkRepository, TeacherMarkRepository>();
            services.AddScoped<ITeacherMarkService, TeacherMarkService>();
            services.AddScoped<IAssignment2Repository, Assignment2Repository>();
            services.AddScoped<IAssignment2Service, Assigment2Service>();

            return services;
        }


    }
}
