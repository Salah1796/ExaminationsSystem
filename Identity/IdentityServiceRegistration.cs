using ExaminationsSystem.Application.Contracts.Identity;
using Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ExaminationsSystem.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentAuthenticationService, StudentAuthenticationService>();
            services.AddScoped<ITokenService, JwtService>();
            services.AddTransient<ICurrentStudentService, CurrentStudentService>();


            return services;
        }
    }
}
