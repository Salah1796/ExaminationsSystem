using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ExaminationsSystem.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AddDbContext
            services.AddDbContext<ExaminationsSystemDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString:ExaminationsSystem"],
                b => b.MigrationsAssembly("ExaminationsSystem.Persistence"));
            });
            #endregion

            #region Repositorys
            services.AddScoped<IExamQuestionRepository, ExamQuestionRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IOptionRepository, OptionRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IStudentAnswerRepository, StudentAnswerRepository>();
            services.AddScoped<IStudentExamRepository, StudentExamRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            #endregion

            #region UnitOfWork
            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
            #endregion

            return services;
        }
    }
}
