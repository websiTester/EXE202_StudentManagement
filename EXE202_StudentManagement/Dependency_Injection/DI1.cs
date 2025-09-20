using EXE202_StudentManagement.Repositories.Class;
using EXE202_StudentManagement.Repositories.Interface;
using EXE202_StudentManagement.Services.Class;
using EXE202_StudentManagement.Services.Interface;

// QUAN

namespace EXE202_StudentManagement.Dependency_Injection
{
    public static class DI1 
    {
        public static IServiceCollection AddMyServices1(this IServiceCollection services)
        {
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
           services.AddScoped<IAssignmentService, AssignmentService>();
            services.AddScoped<IStudentClassRepository, StudentClassRepository>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddScoped<IClassService, ClassService>();
         services.AddScoped<IGroupRepository, GroupRepostiory>();
       services.AddScoped<IGroupService, GroupService>();
       services.AddScoped<IGroupTaskRepository, GroupTaskRepository>();
         services.AddScoped<IGroupTaskService, GroupTaskService>();
        services.AddScoped<IClassRepository, ClassRepository>();
         services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IPeerReviewRepository,PeerReviewRepository>();
            services.AddScoped<IPeerReviewService, PeerReviewService>();
            return services;
        }

    }
}
