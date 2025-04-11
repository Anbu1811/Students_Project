using StudentsDetails.Repository;
using StudentsDetails.Repository.IRepository;

namespace StudentsDetails.Registration
{
    public static class StudentsRegistration
    {
        public static IServiceCollection AddStudentsServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<ICourseRepository,CourseRepository>();
            services.AddScoped<IStudentRepository,StudentRepository>();
            services.AddScoped<IAuthServices,AuthServices>();

            return services;
        }
    }
}
